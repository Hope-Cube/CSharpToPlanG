namespace CSharpToPlanG
{
    internal class Var
    {
        public enum Variable
        {
            Bool,
            Int,
            Foat,
            Char,
            String
        }
        private readonly Variable _variable;
        private readonly string _name;
        private readonly string _value;
        public Var(Variable variable, string name, string value)
        {
            _variable = variable;
            _name = name;
            _value = value;
        }
        internal Variable Variable1 => _variable;
        public string Name => _name;
        public string Value => _value;
    }
}
