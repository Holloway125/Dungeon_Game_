namespace Stats
{
//Different kinds of Operators Flat is +, PercentAdd is sum of all percent modifers, PercentMultiple is product of all percent values 
public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMultiple = 300,
}

public class StatModifer
{

public readonly StatModType Type;
public readonly float Value;
public readonly int Order;
public readonly object Source;
//put in a number, the type of operator you want (flat, percentadd, or percentMultiple), in what order you want, what object the modifer is on
public StatModifer(float value, StatModType type, int order, object source)
{
    Value = value;
    Type = type;
    Order = order;
    Source = source;
}
//Constructors
//takes modifer value and Type of Operator with its default Order position with no reference to an object this will be used for most cases
public StatModifer(float value, StatModType type) : this (value, type, (int)type, null) { }
//takes modifer value and Type of Operator with a specified order with no reference to an object
public StatModifer(float value, StatModType type, int order) : this (value, type, order, null) { }
//takes modifervalue and Type of Operator with its default order position with a referenct to an object
public StatModifer(float value, StatModType type, object source) : this (value, type, (int)type, source) { }



}
}