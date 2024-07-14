using Newtonsoft.Json;

public class CacheAsJsonOnDisk <IKeyType, IValueType> : AICache <IKeyType, IValueType>
{

    public override IValueType Get (IKeyType key)
    {
        return JsonConvert.DeserializeObject<IValueType>(GetRaw(key))!;
    }

    public override void Set (IKeyType key, IValueType value)
    {
        SetRaw(key, JsonConvert.SerializeObject(value));
    }

}