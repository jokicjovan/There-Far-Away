using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Far_Away.Commands
{
    public class CloseDialogCommand : CommandBase
    {
        public CloseDialogCommand() { }

        public override void Execute(object parameter)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
