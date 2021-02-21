using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer
{

    private readonly ICommand command;


    public Drawer(ICommand command)
    {
        this.command = command;
    }


    public void Draw()
    {
        command.Execute();
    }


    public void Erase()
    {
        command.Revert();
    }

}
