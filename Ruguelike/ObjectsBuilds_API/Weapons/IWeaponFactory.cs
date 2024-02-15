using Ruguelike.Weapons;

namespace Ruguelike.ObjectsBuilds_API.Weapons
{
    public interface IWeaponFactory
    {
        IWeapon CreateSword();
        IWeapon CreatePistol();
    }
}
