using System;
public interface ICommand
{
    void Execute();
    void Revert();
    String GetDescription();
}
