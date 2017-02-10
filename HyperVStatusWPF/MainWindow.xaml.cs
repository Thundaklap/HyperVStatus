using HyperVStatus.VirtualMachine;
using HyperVStatus.VirtualMachine.Wmi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HyperVStatus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // The Hyper-V service.
        private IHyperVService service = new WmiHyperVService();

        // The timer which calls refresh events.
        private DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            
            InitializeComponent();

            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Start();
            Top = 3;
            Left = 3;

            Loaded += (a, b) => SetBottom(this);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            VirtualMachines.Children.Clear();
            var controls = service.GetVMs().Select(x => new VirtualMachineControl(x));
            foreach(var control in controls)
            {
                VirtualMachines.Children.Add(control);
            }
        }

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
            int Y, int cx, int cy, uint uFlags);

        const uint SWP_NOSIZE           = 0x0001;
        const uint SWP_NOMOVE           = 0x0002;
        const uint SWP_NOACTIVATE       = 0x0010;
        const uint WM_WINDOWPOSCHANGING = 0x0046;

        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        public static void SetBottom(Window window)
        {
            IntPtr hWnd = new WindowInteropHelper(window).Handle;
            SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        struct tagWINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x, y, cx, cy;
            public uint flags;
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if(msg == WM_WINDOWPOSCHANGING)
            {
                tagWINDOWPOS param = (tagWINDOWPOS)Marshal.PtrToStructure(lParam, typeof(tagWINDOWPOS));
                param.hwndInsertAfter = HWND_BOTTOM;
                Marshal.StructureToPtr(param, lParam, true);
            }

            return IntPtr.Zero;
        }
    }
}
