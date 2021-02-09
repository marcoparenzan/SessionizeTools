namespace SlideComposerApp
{
    using System;
    using System.Text.Json.Serialization;

    public partial class SessionizeData
    {
        [JsonPropertyName("sessions")]
        public Session[] Sessions { get; set; }

        [JsonPropertyName("speakers")]
        public Speaker[] Speakers { get; set; }

        [JsonPropertyName("questions")]
        public object[] Questions { get; set; }

        [JsonPropertyName("categories")]
        public object[] Categories { get; set; }

        [JsonPropertyName("rooms")]
        public Room[] Rooms { get; set; }
    }

    public partial class Room
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sort")]
        public long Sort { get; set; }
    }

    public partial class Session
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("startsAt")]
        public DateTimeOffset StartsAt { get; set; }

        [JsonPropertyName("endsAt")]
        public DateTimeOffset EndsAt { get; set; }

        [JsonPropertyName("isServiceSession")]
        public bool IsServiceSession { get; set; }

        [JsonPropertyName("isPlenumSession")]
        public bool IsPlenumSession { get; set; }

        [JsonPropertyName("speakers")]
        public Guid[] Speakers { get; set; }

        [JsonPropertyName("categoryItems")]
        public object[] CategoryItems { get; set; }

        [JsonPropertyName("questionAnswers")]
        public object[] QuestionAnswers { get; set; }

        [JsonPropertyName("roomId")]
        public long RoomId { get; set; }
    }

    public partial class Speaker
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("bio")]
        public string Bio { get; set; }

        [JsonPropertyName("tagLine")]
        public string TagLine { get; set; }

        [JsonPropertyName("profilePicture")]
        public Uri ProfilePicture { get; set; }

        [JsonPropertyName("isTopSpeaker")]
        public bool IsTopSpeaker { get; set; }

        [JsonPropertyName("links")]
        public object[] Links { get; set; }

        [JsonPropertyName("sessions")]
        public long[] Sessions { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("categoryItems")]
        public object[] CategoryItems { get; set; }

        [JsonPropertyName("questionAnswers")]
        public object[] QuestionAnswers { get; set; }
    }
}
