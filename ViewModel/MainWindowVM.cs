using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        #region Commands
        public ICommand ReadFileCommand { get; private set; }
        #endregion

        #region Fields
        private string readFilePath;
        private string savedFilePath;
        private string input;
        private string output;
        private TextFormatEnum inputFormat;
        #endregion

        #region Properties
        public string Input {
            get
            {
                return input;
            }
            set
            {
                input = value;
                OnPropertyChanged("Input");
            }
        }
        public string Output
        {
            get
            {
                return output;
            }
            set
            {
                output = value;
                OnPropertyChanged("Output");
            }
        }
        public TextFormatEnum InputFormat
        {
            get
            {
                return inputFormat;
            }
            set
            {
                if(value != inputFormat)
                {
                    inputFormat = value;
                    OnPropertyChanged("InputFormat");
                    switch (value)
                    {
                        case TextFormatEnum.Hexadecimal:
                            input = Helpers.ConvertToHexadecimalString(input);
                            break;
                        case TextFormatEnum.PlainText:
                            // !! validate string !!
                            input = Helpers.ConvertFromHexadecimalString(input);
                            break;
                    }
                }
            }
        } 
        #endregion

        public MainWindowVM()
        {
            ReadFileCommand = new RelayCommand(ReadFile, () => true);
        }

        

        private void ReadFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
            {
                readFilePath = dialog.FileName;
                try
                {
                    using (FileStream file = File.OpenRead(readFilePath))
                    {
                         
                    }
                }
                catch(FileNotFoundException e)
                {
                    //logging
                    readFilePath = "File not Found";
                }
                catch(Exception e)
                {
                    //logging and maybe an error message
                }
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
