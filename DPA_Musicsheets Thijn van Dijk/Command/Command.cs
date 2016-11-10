namespace DPA_Musicsheets_Thijn_van_Dijk.Command
{
    public abstract class Command
    {
        public string Name { get; private set; }

        protected Context context;

        protected Command(string name, Context con, CommandRegister reg)
        {
            this.Name = name;
            this.context = con;
            reg.RegisterCommand(Name, this);
        }

        public abstract void Execute();

        public abstract void Undo();

    }
}

