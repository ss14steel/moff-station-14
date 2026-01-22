using Content.Server.Popups;
using Content.Server.Speech.EntitySystems;
using Content.Shared.Abilities.Mime;
using Content.Shared.Chat;
using Content.Shared.Chat.Prototypes;
using Content.Shared.Puppet;
using Content.Shared.Speech;
using Content.Shared.Speech.Muting;

namespace Content.Server.Speech.Muting
{
    public sealed class NonverbalSystem : EntitySystem
    {
        [Dependency] private readonly PopupSystem _popupSystem = default!;
        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<NonverbalComponent, SpeakAttemptEvent>(OnSpeakAttempt);
        }

        private void OnSpeakAttempt(EntityUid uid, NonverbalComponent component, SpeakAttemptEvent args)
        {
            // Only show the popup when the entity is not already muted,
            // so the player does not receive two popup messages.
            if (!HasComp<MutedComponent>(uid))
                _popupSystem.PopupEntity(Loc.GetString("speech-muted"), uid, uid);

            args.Cancel();
        }
    }
}
