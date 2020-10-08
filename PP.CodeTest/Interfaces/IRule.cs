namespace PP.CodeTest
{
    /// <summary>
    /// Generic interface that defines a rule for the system
    /// </summary>
    public interface IRule
    {
        string Code { get; }
        int Order { get; }
    }
}