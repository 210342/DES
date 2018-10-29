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
using System.Windows;
using System.Windows.Input;

namespace DESDe_Encryptor.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        #region Commands
        public ICommand ReadFileCommand { get; private set; }
        public ICommand SaveFileCommand { get; private set; }
        public ICommand StartAction { get; private set; }
        #endregion

        #region Fields
        private string readFilePath;
        private string saveFilePath = null;
        private string input = string.Empty;
        private string output = string.Empty;
        private string keyString = string.Empty;
        private TextFormatEnum inputFormat;
        private TextFormatEnum outputFormat;
        private TextFormatEnum keyFormat;
        private ActionEnum action;
        #endregion

        #region Properties
        public string ReadFilePath
        {
            get
            {
                return readFilePath;
            }
            set
            {
                readFilePath = value;
                OnPropertyChanged("ReadFilePath");
            }
        }
        public string SaveFilePath
        {
            get
            {
                return saveFilePath;
            }
            set
            {
                saveFilePath = value;
                OnPropertyChanged("SaveFilePath");
            }
        }
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
        public string KeyString
        {
            get
            {
                return keyString;
            }
            set
            {
                keyString = value;
                OnPropertyChanged("KeyString");
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
                    
                    switch (value)
                    {
                        case TextFormatEnum.Hexadecimal:
                            inputFormat = value;
                            OnPropertyChanged("InputFormat");
                            Input = Input.ConvertToHexadecimalString();
                            break;
                        case TextFormatEnum.PlainText:
                            if (Input.All("0123456789ABCDEFabcdef -".Contains))
                            {
                                inputFormat = value;
                                OnPropertyChanged("InputFormat");
                                Input = Input.ConvertFromHexadecimalString();
                            }
                            else
                            {
                                MessageBox.Show("Inproper hexadecimal string defined as Input", "Error", MessageBoxButton.OK);
                            }
                            break;
                    }
                }
            }
        }
        public TextFormatEnum OutputFormat
        {
            get
            {
                return outputFormat;
            }
            set
            {
                if(value != outputFormat)
                {
                    switch (value)
                    {
                        case TextFormatEnum.Hexadecimal:
                            Output = Output.ConvertToHexadecimalString();
                            outputFormat = value;
                            OnPropertyChanged("OutputFormat");
                            break;
                        case TextFormatEnum.PlainText:
                            if (Output.All("0123456789ABCDEFabcdef -".Contains))
                            {
                                outputFormat = value;
                                OnPropertyChanged("OutputFormat");
                                Output = Output.ConvertFromHexadecimalString();
                            }
                            else
                            {
                                MessageBox.Show("Inproper hexadecimal string defined as Output", "Error", MessageBoxButton.OK);
                            }
                            break;
                    }
                }
            }
        }
        public TextFormatEnum KeyFormat
        {
            get
            {
                return keyFormat;
            }
            set
            {
                if(value != keyFormat)
                {
                    switch (value)
                    {
                        case TextFormatEnum.Hexadecimal:
                            KeyString = KeyString.ConvertToHexadecimalString();
                            keyFormat = value;
                            OnPropertyChanged("KeyFormat");
                            break;
                        case TextFormatEnum.PlainText:
                            if (KeyString.All("0123456789ABCDEFabcdef -".Contains))
                            {
                                KeyString = KeyString.ConvertFromHexadecimalString();
                                keyFormat = value;
                                OnPropertyChanged("KeyFormat");
                            }
                            else
                            {
                                MessageBox.Show("Inproper hexadecimal string defined as Key", "Error", MessageBoxButton.OK);
                            }
                            break;
                    }
                }
            }
        }
        public ActionEnum Action
        {
            get
            {
                return action;
            }
            set
            {
                if(action != value)
                {
                    action = value;
                    OnPropertyChanged("Action");
                }
            }
        }
        #endregion

        public MainWindowVM()
        {
            OutputFormat = TextFormatEnum.Hexadecimal;
            InputFormat = TextFormatEnum.PlainText;
            KeyFormat = TextFormatEnum.PlainText;
            ReadFilePath = "File not read";
            Action = ActionEnum.Encrypt;

            ReadFileCommand = new RelayCommand(ReadFile, () => true);
            SaveFileCommand = new RelayCommand(SaveToFile, () => !Output.Equals(string.Empty));
            StartAction = new RelayCommand(Start, () => true);

            Output = "4C 6F 72 65 6D 20 69 70 73 75 6D";
        }

        #region Methods (ICommands)
        private void ReadFile()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (dialog.ShowDialog() == true)
            {
                ReadFilePath = dialog.FileName;
                try
                {
                    //Input = string.Join(Environment.NewLine, File.ReadAllLines(ReadFilePath));
                    Input = string.Empty;
                    InputFormat = TextFormatEnum.Hexadecimal;
                    Input = File.ReadAllBytes(ReadFilePath).ConvertToHexadecimalString();
                }
                catch(FileNotFoundException)
                {
                    ReadFilePath = "File not Found";
                    MessageBox.Show("File not Found", "Error", MessageBoxButton.OK);
                }
                catch(DirectoryNotFoundException)
                {
                    ReadFilePath = "Directory not found";
                    MessageBox.Show("Directory not found", "Error", MessageBoxButton.OK);
                }
                catch(UnauthorizedAccessException)
                {
                    ReadFilePath = "Access denied";
                    MessageBox.Show("Access denied", "Error", MessageBoxButton.OK);
                }
                catch (NotSupportedException)
                {
                    ReadFilePath = "Unsupported filesystem";
                    MessageBox.Show("Application supports only NTFS filesystem", "Error", MessageBoxButton.OK);
                }
                catch (Exception) { }
            }
        }

        private void SaveToFile()
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                RestoreDirectory = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                DefaultExt = "txt"
            };
            if (dialog.ShowDialog() == true)
            {
                SaveFilePath = dialog.FileName;
                try
                {
                    switch(OutputFormat)
                    {
                        case TextFormatEnum.Hexadecimal:
                            File.WriteAllBytes(saveFilePath, Encoding.Default.GetBytes(Output.ConvertFromHexadecimalString()));
                            break;
                        case TextFormatEnum.PlainText:
                            File.WriteAllBytes(saveFilePath, Encoding.Default.GetBytes(Output));
                            break;
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("File not Found", "Error", MessageBoxButton.OK);
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("Directory not found", "Error", MessageBoxButton.OK);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Access denied", "Error", MessageBoxButton.OK);
                }
                catch(NotSupportedException)
                {
                    MessageBox.Show("Application supports only NTFS filesystem", "Error", MessageBoxButton.OK);
                }
                catch (Exception) { }
            }
        }

        private void Start()
        {
            try
            {
                Encryptor encryptor = new Encryptor(new Key(ValidateKeyString()));
                byte[] inputBytes;
                switch (InputFormat)
                {
                    case TextFormatEnum.Hexadecimal:
                        inputBytes = Input.ConvertHexadecimalStringToByteArray();
                        break;
                    case TextFormatEnum.PlainText:
                        inputBytes = Encoding.Default.GetBytes(Input);
                        break;
                    default:
                        inputBytes = new byte[] { };
                        break;
                }
                switch (Action)
                {
                    case ActionEnum.Encrypt:
                        encryptor.Encrypt(inputBytes);
                        Output = string.Empty;
                        OutputFormat = TextFormatEnum.Hexadecimal;
                        Output = encryptor.EncryptedMessage.TruncateEndingZeros().ConvertToHexadecimalString();
                        break;
                    case ActionEnum.Decrypt:
                        encryptor.Decrypt(inputBytes);
                        Output = string.Empty;
                        OutputFormat = TextFormatEnum.Hexadecimal;
                        Output = encryptor.DecryptedMessage.TruncateEndingZeros().ConvertToHexadecimalString();
                        break;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Key is too short", "Error", MessageBoxButton.OK);
            }
        }
        #endregion
        
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region private methods
        private byte[] ValidateKeyString()
        {
            byte[] key;
            switch(KeyFormat)
            {
                case TextFormatEnum.PlainText:
                    if(KeyString.Length >= 8)
                    {
                        key = Encoding.Default.GetBytes(KeyString).Take(8).ToArray(); // take 8 first bytes to get 64 bit key
                    }
                    else
                    {
                        throw new Exception("Key is too short");
                    }
                    break;
                case TextFormatEnum.Hexadecimal:
                    byte[] tmp  = KeyString.ConvertHexadecimalStringToByteArray();
                    if (tmp.Count() == 8)
                    {
                        key = tmp;
                    }
                    else
                    {
                        throw new Exception("Key is too short");
                    }
                    break;
                default:
                    key = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    break;
            }
            return key;
        }
        #endregion
    }
}
