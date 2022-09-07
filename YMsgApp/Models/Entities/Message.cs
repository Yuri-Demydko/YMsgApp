using System.ComponentModel;

namespace YMsgApp.Models.Entities;

public class Message:INotifyPropertyChanged
{
    private Guid _id;
    private User _userFrom;
    private string _userFromId;
    private User _userTo;
    private string _userToId;
    private string _text;
    private DateTime _createdAt;
    public event PropertyChangedEventHandler PropertyChanged;

    public Guid Id
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

    public User UserFrom
    {
        get => _userFrom;
        set
        {
            if (_userFrom == value)
            {
                return;
            }

            _userFrom = value;
            
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(_userFrom)));
        }
    }

    public string UserFromId
    {
        get => _userFromId;
        set
        {
            if (_userFromId == value)
            {
                return;
            }

            _userFromId = value;
            
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(_userFromId)));
        }
    }

    public User UserTo
    {
        get => _userTo;
        set
        {
            if (_userTo == value)
            {
                return;
            }

            _userTo = value;
            
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(_userTo)));
        }
    }

    public string UserToId
    {
        get => _userToId;
        set
        {
            if (_userToId == value)
            {
                return;
            }

            _userToId = value;
            
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(_userToId)));
        }
    }

    public string Text
    {
        get => _text;
        set
        {
            if (_text == value)
            {
                return;
            }

            _text = value;
            
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(_text)));
        }
    }

    public DateTime CreatedAt
    {
        get => _createdAt;
        set => _createdAt = value;
    }
}