using Assets;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Table : INotifyPropertyChanged {
    public string ID { get; private set; }
    private Order currOrder;
    private bool imWaiting = false;
    public event PropertyChangedEventHandler PropertyChanged;

    public bool TableAwaiting {
        get
        {
            return imWaiting;
        }

        set {
            this.imWaiting = value;
            //PERHAPS THROW ORDER HERE TO UI HANDLER?
            OnPropertyChanged("TableAwaiting");
        }
    }
    public Table()
    {
        ID = System.Guid.NewGuid().ToString();
    }
    public void SetOrder(Order ord)
    {
        currOrder = ord;
    }
    public void NullifyOrder()
    {
        this.SetOrder(null);
    }

    public bool IsThereOrder()
    {
        return (currOrder == null ? false : true);
    }

    public override bool Equals(object obj)
    {
        var table = obj as Table;
        return table != null &&
               ID == table.ID;
    }
    
    public override int GetHashCode()
    {
        var hashCode = 753895831;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
        return hashCode;
    }

    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
