/*
 * The program checkes whether there is a text in the clipboard.
 * If there is, the the content is displayed along with the 
 * total text length, the last symbol and it's hex code
 * 
 * Author: Konstantin Zhouk, 2023
 */

using System.Windows;
using System.Windows.Input;

namespace Clipboard_Text_Checker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Object to work with the clipboard text
        ClipboardContent cbc = new ClipboardContent();

        // Object to work with the state of the main window
        // In fact, it makes the structure more complicated and could be unnecessary, but
        // it keeps the UI separated from the code
        WindowState ws = new WindowState();

        public MainWindow()
        {
            InitializeComponent();

            // Set data context for the window to control the "Stay on top" state
            wndMain.DataContext = ws;

            // Set data context for the grdi to control the state of the controls
            grdGrid.DataContext = cbc;
        }

        private void btnGetClipboardText_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                cbc.ClipboardString = Clipboard.GetText();
            }
            else
            {
                cbc.RestAllButClipboardString();
                cbc.ClipboardString = null;
            }
        }

        private void chkbxStayOnTop_Checked(object sender, RoutedEventArgs e)
        {
            ws.WindowOnTop = true;
        }

        private void chkbxStayOnTop_Unchecked(object sender, RoutedEventArgs e)
        {
            ws.WindowOnTop = false;
        }

        private void btnResetAll_Click(object sender, RoutedEventArgs e)
        {
            cbc.ResetAll();
        }

        private void wndMain_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key==Key.Escape)
            {
                ws.MainWindowState = System.Windows.WindowState.Minimized;
            }
        }
    }
}
