using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Executor
{

    #region EXECUTOR_SINGLETON_SETUP
    private static Executor _instance;

    public static Executor Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Executor();
            }

            return _instance;
        }
    }
    #endregion

    private List<ICommand> commands;


    public Executor()
    {
        commands = new List<ICommand>();
    }


    public void Execute(ICommand command)
    {
        command.Execute();
        Historicize(command);
    }

    public void Revert()
    {
        if (commands.Count == 0) { return; }

        ICommand command = GetLastCommand();
        command.Revert();
        RemoveLastHistoryEntry();

        Matrix.PrintMatrixToConsole();
    }

    private void Historicize(ICommand command)
    {
        commands.Add(command);
    }


    public void RemoveLastHistoryEntry()
    {
        commands.RemoveAt(commands.Count - 1);
    }


    public ICommand GetLastCommand()
    {
        try
        {
            return commands.Last();
        }
        catch(Exception e)
        {
            Debug.LogWarning("All moves reverted");
            return null;
        }
    }


    public List<ICommand> GetCommands()
    {
        return commands;
    }

}
