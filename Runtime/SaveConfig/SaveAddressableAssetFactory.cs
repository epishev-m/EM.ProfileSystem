namespace EM.Profile
{

using Foundation;

public sealed class SaveAddressableAssetFactory : AddressableAssetFactory<SaveConfig>
{
	#region AddressableAssetFactory

	protected override string Path => nameof(SaveConfig);

	#endregion
}

}