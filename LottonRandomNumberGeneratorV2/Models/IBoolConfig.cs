public interface IBoolConfig
{
    bool IsValue { get; set; }
}

public class BoolConfig : IBoolConfig
{
   public bool IsValue { get; set; }
}