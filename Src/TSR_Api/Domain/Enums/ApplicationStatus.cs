using System.Text.Json.Serialization;

namespace Domain.Enums
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum ApplicationStatus
	{
        Submitted,
        Verified,
        Approved,
        Reserved,
        Expired,
        Refused
    }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum Gender
	{
		Male = 0,
		Female = 1
	}

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum LanguageCourse
	{
		Russian,
		English,
		Tajik
	}

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum SocialMediaType
	{
		LinkedIn,
		GitHub,
		Twitter,
		Facebook,
		Instagram
	}

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum TimelineEventType
	{
		Created = 0,
		Updated = 1,
		Deleted = 2,
		StatusChanged = 3,
		Note = 4,
		Error = 5
	}

}
