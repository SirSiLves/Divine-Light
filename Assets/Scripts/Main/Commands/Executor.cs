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

        PlayerHandler.Instance.TogglePlaying();

        Matrix.PrintMatrixToConsole(Matrix.Instance.GetMatrix());
    }


    private void Historicize(ICommand command)
    {
        commands.Add(command);

        //if (command.GetType() == typeof(MoveCommand))
        //{
        //    CleanUpRotationTry();
        //    commands.Add(command);
        //}
        //else if(command.GetType() == typeof(RotationCommand))
        //{
        //    CleanUpRotationTry();
        //    commands.Add(command);
        //}
        //else if (command.GetType() == typeof(ReplaceCommand))
        //{
        //    CleanUpRotationTry();
        //    commands.Add(command);
        //}
        //else if (command.GetType() == typeof(DestroyCommand))
        //{

        //}

        //Debug.Log("PRINT HISTORY: ");
        //Array.ForEach(commands.ToArray(), c => Debug.Log(c));
    }


    // Add only the last clicked rotation state to history
    private void CleanUpRotationTry()
    {
        if (commands.Count() > 0)
        {
            ICommand command = commands.Last();
            if (command.GetType() == typeof(RotationCommand))
            {
                RemoveLastHistoryEntry();
            }
        }
    }


    public void RemoveLastHistoryEntry()
    {
        commands.RemoveAt(commands.Count - 1);
    }


    public void Revert()
    {
        //TODO

    }


    public List<ICommand> GetCommands()
    {
        return commands;
    }

}
