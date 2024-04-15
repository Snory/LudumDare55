using System;

public class FullFillmentEventArgs : EventArgs
{
    public FullFillment CurrentFullfillment;

    public FullFillmentEventArgs(FullFillment currentFullfillment)
    {
        CurrentFullfillment = currentFullfillment;
    }
}