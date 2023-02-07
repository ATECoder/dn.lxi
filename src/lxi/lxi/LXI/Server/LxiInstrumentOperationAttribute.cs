using System.ComponentModel;

namespace cc.isr.LXI.Server;

/// <summary>   Values that represent XI Instrument operation types. </summary>
public enum LxiInstrumentOperationType
{
    [Description( "Not specified" )] None = 0,
    [Description( "Send message tot he device." )] Write,
    [Description( "Read reply from the device." )] Read
}

/// <summary>
/// IEEE488 Directive tag attributes.
/// </summary>
[AttributeUsage( AttributeTargets.Method )]
public partial class LxiInstrumentOperationAttribute : Attribute
{

    /// <summary>
    /// IEEE488 command content can be marked with full name
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Operation type
    /// </summary>
    public LxiInstrumentOperationType OperationType { get; private set; } = LxiInstrumentOperationType.None;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="content">Instruction content</param>
    /// <param name="operationType">I/O operation type</param>
    public LxiInstrumentOperationAttribute( string content, LxiInstrumentOperationType operationType )
    {
        this.Content = content;
        this.OperationType = operationType;
    }
}
