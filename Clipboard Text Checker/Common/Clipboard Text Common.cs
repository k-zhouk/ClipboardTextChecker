using System;
using System.ComponentModel;

namespace Clipboard_Text_Checker
{
    /// <summary>
    /// This class is used to work with the text context of the clipboard
    /// </summary>
    class ClipboardContent : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// This property contains the text content of the clipboard
        /// </summary>

        private string _clipboardString= string.Empty;
        public string ClipboardString
        {
            get => _clipboardString;

            set
            {
                if (_clipboardString != value)
                {
                    _clipboardString = value;

                    if(!String.IsNullOrEmpty(_clipboardString))
                    {
                        ClipboardStringLength = _clipboardString.Length;
                        // Convert the last char to a string
                        LastClipboardSym = _clipboardString[_clipboardString.Length - 1].ToString();
                        // Convert the last char to a string
                        LastCharHexString = _clipboardString[_clipboardString.Length - 1].ToString();
                    }
                    Notify("ClipboardString");
                }
            }
        }

        /// <summary>
        /// This property contains the length of the clipboard string
        /// </summary>
        private int _clipboardStringLength = default;
        public int ClipboardStringLength
        {
            get => _clipboardStringLength;

            // The setter is used by the class only, so no need to expose it to the outside code
            private set
            {
                _clipboardStringLength = value;
                Notify("ClipboardStringLength");
            }
        }

        /// <summary>
        /// This property contains the last char of the clipboard text.
        /// The last symbol is treated as a string of one character
        /// </summary>
        private string _lastClipboardSym = "N/A";
        public string LastClipboardSym
        {
            get => _lastClipboardSym;

            // The setter is used by the class only, so no need to expose it to the outside code
            private set
            {
                if (_lastClipboardSym != value)
                {
                    if (value == string.Empty)
                    {
                        _lastClipboardSym = "N/A";
                    }
                    else if (!char.IsControl(value[0]))
                    {
                        _lastClipboardSym = "\"" + value[0] + "\"";
                    }
                    else
                    {
                        _lastClipboardSym = "\" \"";
                    }
                }
                Notify("LastClipboardSym");
            }
        }

        /// <summary>
        /// This string represents the hex code of the last char of the clipboard string
        /// </summary>
        private string _lastCharHexString = "N/A";
        public string LastCharHexString
        {
            get => _lastCharHexString;

            // The setter is used by the class only, so no need to expose it to the outside code
            private set
            {
                if (_lastCharHexString != value)
                {
                    if (!String.IsNullOrEmpty(_clipboardString))
                    {
                        _lastCharHexString = string.Format("0x" + "{0:X}", (int)_clipboardString[_clipboardString.Length - 1]);
                    }
                    if(value==string.Empty)
                    {
                        _lastCharHexString = "N/A";
                    }
                }
                Notify("LastCharHexString");
            }
        }

        public void ResetAll()
        {
            ClipboardString = string.Empty;
            ClipboardStringLength = 0;
            LastClipboardSym = string.Empty;
            LastCharHexString = string.Empty;
        }

        public void RestAllButClipboardString()
        {
            ClipboardStringLength = 0;
            LastClipboardSym = string.Empty;
            LastCharHexString = string.Empty;
        }
    }

    /// <summary>
    /// This calss is used to control the states of the Main Window:
    /// 1. "Stay on top" state
    /// 2. Minimized state once the ESC key is pressed
    /// </summary>
    class WindowState : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _windowOnTop = default;
        public bool WindowOnTop
        {
            get => _windowOnTop;
            set { _windowOnTop = value; Notify("WindowOnTop"); }
        }

        private System.Windows.WindowState _mainWindowState;
        public System.Windows.WindowState MainWindowState
        {
            get => _mainWindowState;
            set { _mainWindowState = value; Notify("MainWindowState"); }
        }
    }
}
