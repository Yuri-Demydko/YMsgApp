using System.ComponentModel;

namespace YMsgApp.Models.Entities;

public class LoginModel:INotifyPropertyChanged
{
    private string _displayName;
    private string _userName;
    private string _password;

    public event PropertyChangedEventHandler PropertyChanged;
    

    public string UserName
    {
        get => _userName;
        set
        {
            if (_userName == value)
            {
                return;
            }

            _userName = value;
            
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(_userName)));
        }
    }

    public string? DisplayName
    {
        get => _displayName;
        set
        {
            if (_displayName == value)
            {
                return;
            }

            _displayName = value;
            
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(_displayName)));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (_password == value)
            {
                return;
            }

            _password = value;
            
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(_password)));
        }
    }
}