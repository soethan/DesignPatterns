using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns
{
    //http://www.codeproject.com/Articles/328373/Understanding-and-Implementing-Abstract-Factory-Pa
    enum MANUFACTURER
    {
        NOKIA,
        SONYERICSSON
    }

    enum PHONETYPE
    {
        NORMAL,
        SMART
    }

    /// <summary>
    /// Client
    /// </summary>
    class PhoneClient
    {
        ISmartPhone _smartPhone;
        INormalPhone _normalPhone;
        IPhoneFactory factory;

        public PhoneClient(MANUFACTURER manufacturer)
        {
            switch (manufacturer)
            {
                case MANUFACTURER.NOKIA:
                    factory = new NokiaFactory();
                    break;
                case MANUFACTURER.SONYERICSSON:
                    factory = new SonyEricssonFactory();
                    break;
                default:
                    throw new ArgumentException("Invalid Manufacturer");
            }
        }

        public object GetPhone(PHONETYPE phoneType)
        {
            switch (phoneType)
            {
                case PHONETYPE.SMART:
                    return factory.GetSmartPhone();
                case PHONETYPE.NORMAL:
                default:
                    return factory.GetNormalPhone();
            }
        }
    }

    /// <summary>
    /// Abstract Factory
    /// </summary>
    interface IPhoneFactory
    {
        ISmartPhone GetSmartPhone();
        INormalPhone GetNormalPhone();
    }

    /// <summary>
    /// Concrete Factory
    /// </summary>
    class NokiaFactory : IPhoneFactory
    {
        public INormalPhone GetNormalPhone()
        {
            return new NokiaNormalPhone();
        }

        public ISmartPhone GetSmartPhone()
        {
            return new NokiaSmartPhone();
        }
    }

    /// <summary>
    /// Concrete Factory
    /// </summary>
    class SonyEricssonFactory : IPhoneFactory
    {
        public INormalPhone GetNormalPhone()
        {
            return new SonyEricssonNormalPhone();
        }

        public ISmartPhone GetSmartPhone()
        {
            return new SonyEricssonSmartPhone();
        }
    }

    /// <summary>
    /// Abstract Product
    /// </summary>
    interface ISmartPhone
    {
        string Name();
    }

    /// <summary>
    /// Abstract Product
    /// </summary>
    interface INormalPhone
    {
        string Name();
    }

    /// <summary>
    /// Concrete Product
    /// </summary>
    class NokiaSmartPhone : ISmartPhone
    {
        public string Name()
        {
            return "NokiaSmartPhone";
        }
    }

    /// <summary>
    /// Concrete Product
    /// </summary>
    class NokiaNormalPhone : INormalPhone
    {
        public string Name()
        {
            return "NokiaNormalPhone";
        }
    }

    /// <summary>
    /// Concrete Product
    /// </summary>
    class SonyEricssonSmartPhone : ISmartPhone
    {
        public string Name()
        {
            return "SonyEricssonSmartPhone";
        }
    }

    /// <summary>
    /// Concrete Product
    /// </summary>
    class SonyEricssonNormalPhone : INormalPhone
    {
        public string Name()
        {
            return "SonyEricssonNormalPhone";
        }
    }
}
