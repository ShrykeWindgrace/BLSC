using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLSC
{
    public abstract class Command
    {
        public abstract void Execute();
    }

    public abstract class UndoableCommand : Command
    {
        public abstract void Undo();
    }
    public class ChangeCBO : UndoableCommand
    {
        //private Field _field;
        private EFType newType;
        private EFType oldType;
        private FieldWrap _fw;
        public ChangeCBO(FieldWrap fw, EFType et)
        {
            _fw = fw;
            oldType = fw._f.type.t;
            newType = et;
        }

        public override void Execute()
        {
            //throw new NotImplementedException();
            _fw._f.type.t = newType;
        }
        public override void Undo()
        {
            //throw new NotImplementedException();
            _fw._f.type.t = oldType;
        }
    }
    public class FieldWrap
    {
        public Field _f;

        public FieldWrap(Field f)
        {
            _f = f;
        }
        public void SwitchIndex(int i)
        {
            //donothing
        }
    }
    public class CommandManager
    {
        private Stack commandStack = new Stack();

        public void ExecuteCommand(Command cmd)
        {
            cmd.Execute();
            if (cmd is UndoableCommand)
            {
                commandStack.Push(cmd);
            }
        }

        public void Undo()
        {
            if (commandStack.Count > 0)
            {
                UndoableCommand cmd = (UndoableCommand)commandStack.Pop();
                cmd.Undo();
            }
        }
    }

}
