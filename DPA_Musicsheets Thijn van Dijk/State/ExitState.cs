using System;

namespace DPA_Musicsheets_Thijn_van_Dijk.State
{
    public abstract class ExitState : global::DPA_Musicsheets_Thijn_van_Dijk.State.State
    {
        public override void Handle(Context con) {}
        public virtual void save(Context con) {/*Empty to make subclasses nicer*/}
        public virtual void edit(Context con) {/*Empty to make subclasses nicer*/}
        public virtual void HandleClose(Context con, System.ComponentModel.CancelEventArgs e) { /*Empty to make subclasses nicer*/ }
    }
}

