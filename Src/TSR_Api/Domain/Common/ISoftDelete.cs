namespace Domain.Common
{
	internal interface ISoftDelete
	{
		bool IsDeleted { get; set; }
	}
}
