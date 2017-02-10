using HyperVStatus.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HyperVStatus
{
    /// <summary>
    /// Interaction logic for VirtualMachineControl.xaml
    /// </summary>
    // TODO: refactor this.
    public partial class VirtualMachineControl : UserControl
    {
        private IVirtualMachine _machine;
        internal IVirtualMachine Machine {
            get
            {
                return _machine;
            }
            set
            {
                _machine = value;
                Update();
            }
        }

        internal string Status
        {
            get
            {
                return Machine.State.ToString();
            }
        }
        
        public VirtualMachineControl(IVirtualMachine machine)
        {
            DataContext = this;
            InitializeComponent();
            Machine = machine;
            VMAction.Click += VMAction_Click;
        }

        private void VMAction_Click(object sender, RoutedEventArgs e)
        {
            var action = GetButtonAction();
            if(action.HasValue)
            {
                Machine.RequestStateChange(action.Value.Action);
                // now we should hide the button to deactivate.
                VMAction.Visibility = Visibility.Hidden;
            }
        }

        private struct ButtonAction
        {
            public string ActionText { get; set; }
            public StateChange Action { get; set; }

            internal ButtonAction(string actionText, StateChange action)
            {
                ActionText = actionText;
                Action = action;
            }
        }

        private Dictionary<EnabledState, ButtonAction> map = new Dictionary<EnabledState, ButtonAction>()
        {
            [EnabledState.Disabled] = new ButtonAction("Power on", StateChange.Enabled),
            [EnabledState.Enabled] = new ButtonAction("Power off", StateChange.Disabled),
            [EnabledState.Paused] = new ButtonAction("Resume", StateChange.Enabled),
            [EnabledState.Suspended] = new ButtonAction("Resume", StateChange.Enabled),
        };

        private ButtonAction? GetButtonAction()
        {
            var state = Machine.State;
            if(!map.ContainsKey(state))
            {
                return null;
            }

            return map[state];
        }

        void Update()
        {
            VMName.Text = Machine.Name;
            VMStatus.Text = Machine.State.ToString();
            VMUptime.Text = Machine.Uptime.Equals(TimeSpan.Zero) ? "" : Machine.Uptime.ToString("g");
            var buttonAction = GetButtonAction();
            if(buttonAction.HasValue)
            {
                VMAction.Visibility = Visibility.Visible;
                ActionText.Text = buttonAction.Value.ActionText;
            }
            else
            {
                VMAction.Visibility = Visibility.Hidden;
            }
        }
    }
}
