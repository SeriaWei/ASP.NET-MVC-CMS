/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.IOC;

namespace Easy.Web.ValueProvider
{
    public interface ICookie : IDependency
    {
        T GetValue<T>(string name);

        T GetValue<T>(string name, bool expireOnceRead);

        void SetValue<T>(string name, T value);

        void SetValue<T>(string name, T value, float expireDurationInMinutes);

        void SetValue<T>(string name, T value, bool httpOnly, bool expireWithBrowser);

        void SetValue<T>(string name, T value, float expireDurationInMinutes, bool httpOnly, bool expireWithBrowser);
    }
}