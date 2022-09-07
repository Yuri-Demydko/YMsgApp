using System.ComponentModel;

namespace YMsgApp.Models.Entities;

public class User:INotifyPropertyChanged
{
    private string _id;
    private string _displayName;
    private string _userName;

    public event PropertyChangedEventHandler PropertyChanged;
    public string Id
    {
        get => _id;
        set
        {
            if (_id == value)
            {
                return;
            }

            _id = value;
            
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(_id)));
        }
    }

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

}