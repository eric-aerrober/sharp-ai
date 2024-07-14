using Newtonsoft.Json;

public class CacheDontCache <IKeyType, IValueType> : AICache <IKeyType, IValueType>
{
    public override bool Has (IKeyType key)
    {
        return false;
    }

}