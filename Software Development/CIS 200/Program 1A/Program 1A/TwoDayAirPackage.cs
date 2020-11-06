// Program 1A
// CIS 200-01
// Fall 2019
// Due: 9/23/2019
// By: M1791

// File: TwoDayAirPackage.cs
// Derived from the air package class. Two day air packages includes a 
// delivery type enumerable and cost calculation

using System;
using System.Collections.Generic;
using System.Text;

namespace Program_1A
{
    public class TwoDayAirPackage : AirPackage
    {
        // Backing Fields
        private Delivery _deliveryType; // Delivery type value

        // Constants
        const double SHIPPING_CHARGE = 0.20; // Shipping charge constant

        public enum Delivery { Early, Saver }; // Delivery enumerable

        // Precondition:  Length, width, height, and weight > 0
        // Postcondition: The package is created with the specified values for origin address, 
        //                destination address, length, width, height, and weight, delivery type
        public TwoDayAirPackage(Address originAddress, Address destAddress, double length,
                       double width, double height, double weight, Delivery deliveryType)
            : base(originAddress, destAddress, length, width, height, weight)
        {
            DeliveryType = deliveryType;
        }

        public Delivery DeliveryType
        {
            // Precondition:  None
            // Postcondition: The two day air package's delivery type has been returned
            get
            {
                return _deliveryType;
            }
            // Precondition:  None
            // Postcondition: The delivery type has been set to the specified value
            set
            {
                _deliveryType = value;
            }
        }

        // Precondition:  None
        // Postcondition: The two day air package's cost has been returned
        public override decimal CalcCost()
        {
            double baseCost = SHIPPING_CHARGE * (Length + Width + Height) + SHIPPING_CHARGE * (Weight);

            if (DeliveryType == Delivery.Saver)
            {
                double finalCost = baseCost * 0.85;
                return (decimal)finalCost;
            }
            else
            {
                return (decimal)baseCost;
            }
        }

        // Precondition:  None
        // Postcondition: A String with the two day air package's data has been returned
        public override string ToString()
        {
            return $"Two Day Air Package\n\n{base.ToString()}" +
                   $"Delivery Type: {DeliveryType}\n";
        }
    }
}
