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
        private FieldWrapO _fw;
        public ChangeCBO(FieldWrapO fw, EFType et)
        {
            _fw = fw;
            oldType = fw.FieldType;
            newType = et;
        }

        public override void Execute()
        {
            //throw new NotImplementedException();
            _fw.SwitchToType(newType);
        }
        public override void Undo()
        {
            //throw new NotImplementedException();
            _fw.SwitchToType(oldType);
        }
    }
    public class ChangeCBP : UndoableCommand
    {
        //private Field _field;
        private EPunct newType;
        private EPunct oldType;
        private FieldWrapP _fw;
        public ChangeCBP(FieldWrapP fw, EPunct ep)
        {
            _fw = fw;
            oldType = fw.ep;
            newType = ep;
        }

        public override void Execute()
        {
            //throw new NotImplementedException();
            _fw.SwitchToType(newType);
        }
        public override void Undo()
        {
            //throw new NotImplementedException();
            _fw.SwitchToType(oldType);
        }
    }
    public class FieldWrapO
    {
        private Field _f;
        public EFType FieldType
        {
            get
            {
                return _f.type.t;
            }
            set
            {
                _f.type.t = value;
            }
        }
        public FieldWrapO(Field f)
        {
            _f = f;
        }
        public void SwitchToType(EFType eft)
        {
            FieldType = eft;
        }
    }
    public class FieldWrapP
    {
        private Field _f;
        public EPunct ep
        {
            get
            {
                return _f.ps.p.p;
            }
            set
            {
                _f.ps.p.p = value;
            }
        }
        public FieldWrapP(Field f)
        {
            _f = f;
        }
        public void SwitchToType(EPunct eft)
        {
            ep = eft;
        }
    }
    public class CommandManager
    {
        private Stack commandStack = new Stack();
        public int count
        {
            get
            {
                return commandStack.Count;
            }
        }

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
