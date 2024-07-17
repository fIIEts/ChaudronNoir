using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChaudronNoir.Classes;

public class Stats : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public int _habilete;
    public int _adresse;
    public int _endurance;
    public int _chanceMax;
    public int _chanceAct;
    public int _pvMax;
    public int _pvAct;
    public int _armure;
    public int _degats;
    public int _critique;
    public int _gloire;
    public int _richesse;

    public int Habilete
    {
        get => _habilete;
        set
        {
            if (_habilete != value)
            {
                _habilete = value;
                OnPropertyChanged();
            }
        }
    }
    public int Adresse
    {
        get => _adresse;
        set
        {
            if (_adresse != value)
            {
                if (value > 5)
                {
                    _adresse = 5;
                }
                else
                {
                    _adresse = value;
                }
                OnPropertyChanged();
            }
        }
    }
    public int Endurance
    {
        get => _endurance;
        set
        {
            if (_endurance != value)
            {
                // Si on augmente notre endurance, on augmente nos pv max
                PVMax += (value - _endurance)*3;

                _endurance = value;
                OnPropertyChanged();
            }
        }
    }
    public int ChanceMax
    {
        get => _chanceMax;
        set
        {
            if (_chanceMax != value)
            {
                int oldchMax = _chanceMax;

                _chanceMax = value;
                OnPropertyChanged();

                // Si on augmente notre chance on augmente aussi la chance act
                if (oldchMax <= value)
                {
                    ChanceAct += (value - oldchMax);
                }

                // Si on diminue notre chance, on ne peux pas dépasser notre chance max
                else if (ChanceAct > value)
                {
                    ChanceAct = value;
                }


            }
        }
    }
    public int ChanceAct
    {
        get => _chanceAct;
        set
        {
            if (_chanceAct != value)
            {
                if (value > _chanceMax)
                {
                    value = _chanceMax;
                }

                _chanceAct = value;
                OnPropertyChanged();
            }
        }
    }
    public int PVMax
    {
        get => _pvMax;
        set
        {
            if (_pvMax != value)
            {
                int oldpvMax = _pvMax;

                _pvMax = value;
                OnPropertyChanged();

                // Si on augmente nos pv on augmente aussi les pv act
                if (oldpvMax <= value)
                {
                    PVAct += (value - oldpvMax);
                }

                // Si on diminue nos pv, on ne peux pas dépasser nos pv max
                else if (PVAct > value)
                {
                    PVAct = value;
                }

            }
        }
    }
    public int PVAct
    {
        get => _pvAct;
        set
        {
            if (_pvAct != value)
            {
                if (value > _pvMax)
                {
                    value = _pvMax;
                }

                _pvAct = value;
                OnPropertyChanged();
            }
        }
    }
    public int Armure
    {
        get => _armure;
        set
        {
            if (_armure != value)
            {
                _armure = value;
                OnPropertyChanged();
            }
        }
    }
    public int Degats
    {
        get => _degats;
        set
        {
            if (_degats != value)
            {
                _degats = value;
                OnPropertyChanged();
            }
        }
    }
    public int Critique
    {
        get => _critique;
        set
        {
            if (_critique != value)
            {
                _critique = value;
                OnPropertyChanged();
            }
        }
    }
    public int Gloire
    {
        get => _gloire;
        set
        {
            if (_gloire != value)
            {
                _gloire = value;
                OnPropertyChanged();
            }
        }
    }
    public int Richesse
    {
        get => _richesse;
        set
        {
            if (_richesse != value)
            {
                _richesse = value;
                OnPropertyChanged();
            }
        }
    }

    public Stats()
    {
        _habilete = 2;
        _adresse = 1;
        _endurance = 2;
        _pvMax = _endurance * 3;
        _pvAct = _pvMax;
        _chanceMax = 3;
        _chanceAct = _chanceMax;
        _armure = 0;
        _degats = 0;
        _critique = 0;
        _gloire = 0;
        _richesse = 0;
    }

    public void Reset()
    {
        Habilete = 2;
        Adresse = 1;
        Endurance = 2;
        PVMax = Endurance * 3;
        ChanceMax = 3;
        Armure = 0;
        Degats = 0;
        Critique = 0;
        Gloire = 0;
        Richesse = 0;
        OnPropertyChanged();
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
