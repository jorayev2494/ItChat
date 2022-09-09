using ItChat.Models.Contracts;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Models
{
    public class User : ObservableObject, IEntity
    {
        [JsonProperty("id")]
        public uint Id { get; set; }

        private string? firstName;

        [JsonProperty("first_name")]
        public string? FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }

        private string? lastName;

        [JsonProperty("last_name")]
        public string? LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        [JsonProperty("full_name")]
        public string? FullName { get; set; }

        private string email;

        [JsonProperty("email")]
        public string? Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string? avatar;

        [JsonProperty("avatar")]
        public string? Avatar
        {
            get { return avatar; }
            set { SetProperty(ref avatar, value); }
        }

        private string phone;

        [JsonProperty("phone")]
        public string Phone
        {
            get { return phone; }
            set { SetProperty(ref phone, value); }
        }

        private int? phoneCountryId;

        [JsonProperty("phone_country_id")]
        public int? PhoneCountryId
        {
            get { return phoneCountryId; }
            set { SetProperty(ref phoneCountryId, value); }
        }

        private Country? phoneCountry;

        [JsonProperty("phone_country")]
        public Country? PhoneCountry
        {
            get { return phoneCountry; }
            set { SetProperty(ref phoneCountry, value); }
        }

        private int? countryId;

        [JsonProperty("country_id")]
        public int? CountryId
        {
            get { return countryId; }
            set { SetProperty(ref countryId, value); }
        }

        private Country? country;

        [JsonProperty("country")]
        public Country? Country
        {
            get { return country; }
            set { SetProperty(ref country, value); }
        }

        private bool isAdmin;

        [JsonIgnore, JsonProperty("is_admin")]
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { SetProperty(ref isAdmin, value); }
        }

        private bool isPrivate = false;

        [JsonIgnore, JsonProperty("is_private")]
        public bool IsPrivate
        {
            get { return isPrivate; }
            set { SetProperty(ref isPrivate, value); }
        }

        private string createdAt;

        [JsonProperty("created_at")]
        public string CreatedAt
        {
            get { return createdAt; }
            set { SetProperty(ref createdAt, value); }
        }

        private string updatedAt;

        [JsonProperty("updated_at")]
        public string UpdatedAt
        {
            get { return updatedAt; }
            set { SetProperty(ref updatedAt, value); }
        }
    }

    //public class User : BaseModel // , IEntity
    //{
    //    private uint id;

    //    [JsonProperty("id")]
    //    public uint Id
    //    {
    //        get { return id; }
    //        set { SetProperty(ref id, value); }
    //    }

    //    private string? firstName;

    //    [JsonProperty("first_name")]
    //    public string? FirstName
    //    {
    //        get { return firstName; }
    //        set { SetProperty(ref firstName, value); }
    //    }

    //    private string? lastName;

    //    [JsonProperty("last_name")]
    //    public string? LastName
    //    {
    //        get { return lastName; }
    //        set { SetProperty(ref lastName, value); }
    //    }

    //    [JsonProperty("full_name")]
    //    public string? FullName { get; set; }

    //    private string email;

    //    [JsonProperty("email")]
    //    public string? Email
    //    {
    //        get { return email; }
    //        set { SetProperty(ref email, value); }
    //    }

    //    private string? avatar;

    //    [JsonProperty("avatar")]
    //    public string? Avatar
    //    {
    //        get { return avatar; }
    //        set { SetProperty(ref avatar, value); }
    //    }

    //    private string phone;

    //    [JsonProperty("phone")]
    //    public string Phone
    //    {
    //        get { return phone; }
    //        set { SetProperty(ref phone, value); }
    //    }

    //    private int? phoneCountryId;

    //    [JsonProperty("phone_country_id")]
    //    public int? PhoneCountryId
    //    {
    //        get { return phoneCountryId; }
    //        set { SetProperty(ref phoneCountryId, value); }
    //    }

    //    private Country phoneCountry;

    //    [JsonProperty("phone_country")]
    //    public Country PhoneCountry
    //    {
    //        get { return PhoneCountry; }
    //        set { SetProperty(ref phoneCountry, value); }
    //    }

    //    private int? countryId;

    //    [JsonProperty("country_id")]
    //    public int? CountryId
    //    {
    //        get { return countryId; }
    //        set { SetProperty(ref countryId, value); }
    //    }

    //    private Country? country;

    //    [JsonProperty("country")]
    //    public Country? Country
    //    {
    //        get { return country; }
    //        set { SetProperty(ref country, value); }
    //    }

    //    private bool isAdmin;

    //    [JsonIgnore, JsonProperty("is_admin")]
    //    public bool IsAdmin
    //    {
    //        get { return isAdmin; }
    //        set { SetProperty(ref isAdmin, value); }
    //    }

    //    private bool isPrivate = false;

    //    [JsonIgnore, JsonProperty("is_private")]
    //    public bool IsPrivate
    //    {
    //        get { return isPrivate; }
    //        set { SetProperty(ref isPrivate, value); }
    //    }

    //    private string createdAt;

    //    [JsonProperty("created_at")]
    //    public string CreatedAt
    //    {
    //        get { return createdAt; }
    //        set { SetProperty(ref createdAt, value); }
    //    }

    //    private string updatedAt;

    //    [JsonProperty("updated_at")]
    //    public string UpdatedAt
    //    {
    //        get { return updatedAt; }
    //        set { SetProperty(ref updatedAt, value); }
    //    }
    //}
}
