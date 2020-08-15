using System;

namespace ShoppingCart.Business.Domain
{
    public struct Price
    {
        public decimal Amount { get; }

        public Currency Currency { get; }

        public Price(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Price operator *(Price p1, int quantity)
        {
            return new Price(p1.Amount * quantity, p1.Currency);
        }

        public static Price operator +(Price p1, Price p2)
        {
            if (p1.Currency != p2.Currency)
                throw new ArgumentException("cannot sum prices belong to different currencies");

            return new Price(p1.Amount + p2.Amount, p1.Currency);
        }

        public bool Equals(Price other)
        {
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            return Equals((Price) obj);
        }

        public override int GetHashCode()
        {
            return (Amount.GetHashCode() * 397) ^ (int) Currency;
        }
    }

    public enum Currency
    {
        Unknown = 0,
        TL
    }
}