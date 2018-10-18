using Assets;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public class Table : INotifyPropertyChanged
{
    public string ID { get; private set; }
    private Order currOrder;
    private bool imWaiting = false;
    public event PropertyChangedEventHandler PropertyChanged;
    private int beersOnTable = 0;

    public float Mood = 20;



    public bool TableAwaiting
    {
        get
        {
            return imWaiting;
        }

        set
        {
            this.imWaiting = value;
            //PERHAPS THROW ORDER HERE TO UI HANDLER?
            OnPropertyChanged("TableAwaiting");
        }
    }
    public Table()
    {
        ID = System.Guid.NewGuid().ToString();
    }

    public Order CurrOrder
    {
        get
        {
            return currOrder;
        }

        set
        {
            this.currOrder = value;
            OnPropertyChanged("CurrOrder");
        }
    }

    public void NullifyOrder()
    {
        //        this.SetOrder(null);

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

    public void ControlOrder(float currentTime)
    {

        if (beersOnTable < currOrder.getOrderSize())
        {
            if (currOrder.getEndTime() > currentTime)
            {
                //Obniż stopień zadowolenia stolika i ogólego z zależności od różnicy
                //wyzeruj winstreak;
            }
            else if (currOrder.getEndTime() <= currentTime)
            {
                //DO NOTHING
            }
        }
        else if (beersOnTable == currOrder.getOrderSize())
        {
            //DODAJ PUNKTY I WIN STREAKA
        }
    }
}
