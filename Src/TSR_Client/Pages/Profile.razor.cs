using Blazorise.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TSR_Accoun_Application.Contracts.Educations.Command.Create;
using TSR_Accoun_Application.Contracts.Educations.Command.Update;
using TSR_Accoun_Application.Contracts.Educations.Responsess;
using TSR_Accoun_Application.Contracts.Profile.Commands.UpdateProfile;
using TSR_Accoun_Application.Contracts.Profile.Responses;
using TSR_Accoun_Application.Contracts.Profile;
using TSR_Client.Components.Dialogs;
using TSR_Client.Identity;
using TSR_Client.Services.Auth;

namespace TSR_Client.Pages
{
	public partial class Profile
	{
		[Inject] private IAuthService AuthService { get; set; }

		private bool _processing;
		private bool _tryButton;
		private bool _codeSent;
		private int? _confirmationCode;
		private bool _isPhoneNumberNull = true;

		private int _active;

		private void ActiveNavLink(int active)
		{
			if (_profile != null)
			{
				_active = active;
				return;
			}

			ServerNotResponding();
		}

		private UserProfileResponse _profile;
		private readonly UpdateProfileCommand _updateProfileCommand = new UpdateProfileCommand();

		private async Task SendCode()
		{
			bool response = await UserProfileService.SendConfirmationCode(_profile.PhoneNumber);
			if (response) _codeSent = true;
		}

		protected override async void OnInitialized()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;

			if (!user.Identity.IsAuthenticated)
			{
				Navigation.NavigateTo("sign-in");
				return;
			}

			await Load();
		}

		private async Task Load()
		{
			_tryButton = false;
			StateHasChanged();


			try
			{
				_profile = await UserProfileService.Get();

				_updateProfileCommand.AboutMyself = _profile.AboutMyself;
				_updateProfileCommand.DateOfBirth = _profile.DateOfBirth;
				_updateProfileCommand.Email = _profile.Email;
				_updateProfileCommand.FirstName = _profile.FirstName;
				_updateProfileCommand.LastName = _profile.LastName;
				_updateProfileCommand.Gender = _profile.Gender;
				_updateProfileCommand.PhoneNumber = _profile.PhoneNumber;
				_isPhoneNumberNull = !_updateProfileCommand.PhoneNumber.IsNullOrEmpty();
			}
			catch (Exception)
			{
				ServerNotResponding();
				_tryButton = true;
				StateHasChanged();
			}


			await GetEducations();


			StateHasChanged();
		}
		private async Task Verify()
		{ 
		}
         private async Task SendEmailConfirms()
        {
          
        }
        private void ServerNotResponding()
		{
			Snackbar.Add("Server is not responding, please try later", MudBlazor.Severity.Error);
		}

		private async Task BadRequestResponse(HttpResponseMessage response)
		{
			var customProblemDetails = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();
			if (customProblemDetails.Detail != null)
			{
				Snackbar.Add(customProblemDetails.Detail, MudBlazor.Severity.Error);
			}
			else
			{
				var errorResponseString = await response.Content.ReadAsStringAsync();
				var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorResponseString);
				foreach (var error in errorResponse.Errors)
				{
					var errorMessage = string.Join(", ", error.Value);
					Snackbar.Add(errorMessage, MudBlazor.Severity.Error);
				}
			}
		}

		private async Task ConfirmDelete<T>(IList<T> collection, T item)
		{
			var parameters = new DialogParameters<DialogMudBlazor>();
			parameters.Add(x => x.ContentText,
				"Do you really want to delete these records? This process cannot be undone.");
			parameters.Add(x => x.ButtonText, "Delete");
			parameters.Add(x => x.Color, Color.Error);

			var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

			var dialog = DialogService.Show<DialogMudBlazor>("Delete", parameters, options);
			var result = await dialog.Result;

			if (!result.Canceled)
			{
				Delete(collection, item);
			}
		}

		private async void Delete<T>(ICollection<T> collection, T item)
		{
			if (collection.Contains(item))
			{
				Type itemType = item.GetType();
				var idProperty = itemType.GetProperty("Id");
				var result = new HttpResponseMessage();
				if (idProperty != null)
				{
					try
					{
						if (itemType.Name == "String")
						{

						}
						else
						{
							Guid id = (Guid)idProperty.GetValue(item);

							switch (itemType.Name)
							{
								case "UserEducationResponse":
									result = await UserProfileService.DeleteEducationAsync(id);
									break;
								default:
									break;
							}
						}
					}
					catch (HttpRequestException)
					{
						ServerNotResponding();
						return;
					}

					if (result.IsSuccessStatusCode)
					{
						collection.Remove(item);
						StateHasChanged();
					}
				}
			}
		}

		private async void UpdateProfile()
		{
			_processing = true;
			var result = await UserProfileService.Update(_updateProfileCommand);
			if (result == "")
			{
				Snackbar.Add("Profile updated successfully.", MudBlazor.Severity.Success);
				_profile = await UserProfileService.Get();
			}
			else
			{
				Snackbar.Add(result, MudBlazor.Severity.Error);
			}

			_processing = false;
			StateHasChanged();
		}

		#region education

		List<UserEducationResponse> educations = new List<UserEducationResponse>();
		private bool addEducation = false;
		CreateEducationDetailCommand createEducation = new CreateEducationDetailCommand();
		List<UserEducationResponse> allEducctions = new List<UserEducationResponse>();


		Guid editingCardId = new Guid();
		UpdateEducationDetailCommand educationUpdate = null;

		private async Task GetEducations()
		{
			try
			{
				educations = await UserProfileService.GetEducationsByUser();
				allEducctions = await UserProfileService.GetAllEducations();
			}
			catch (Exception)
			{
				ServerNotResponding();
			}
		}

		private async Task<IEnumerable<string>> SearchEducation(string value)
		{
			await Task.Delay(5);

			return allEducctions.Where(e => e.Speciality
					.Contains(value, StringComparison.InvariantCultureIgnoreCase))
				.Select(e => e.Speciality).Distinct()
				.ToList();
		}

		private async Task<IEnumerable<string>> SearchUniversity(string value)
		{
			await Task.Delay(5);

			return allEducctions.Where(e => e.University
					.Contains(value, StringComparison.InvariantCultureIgnoreCase))
				.Select(e => e.University).Distinct()
				.ToList();
		}

		private void EditButtonClicked(Guid cardId, UserEducationResponse educationResponse)
		{
			editingCardId = cardId;
			educationUpdate = new UpdateEducationDetailCommand()
			{
				EndDate = educationResponse.EndDate.HasValue ? educationResponse.EndDate.Value : default(DateTime),
				StartDate =
					educationResponse.StartDate.HasValue ? educationResponse.StartDate.Value : default(DateTime),
				University = educationResponse.University,
				Speciality = educationResponse.Speciality,
				Id = educationResponse.Id,
				UntilNow = educationResponse.UntilNow
			};
		}

		private async Task CreateEducationHandle()
		{
			_processing = true;
			try
			{
				if (createEducation.EndDate == null)
					createEducation.EndDate = default(DateTime);

				var response = await UserProfileService.CreateEducationAsуnc(createEducation);
				if (response.IsSuccessStatusCode)
				{
					Snackbar.Add("Add Education successfully.", MudBlazor.Severity.Success);
					addEducation = false;
					createEducation = new CreateEducationDetailCommand();
					await GetEducations();
					StateHasChanged();
				}
				else if (response.StatusCode == HttpStatusCode.BadRequest)
				{
					await BadRequestResponse(response);
				}
			}
			catch (HttpRequestException)
			{
				ServerNotResponding();
			}

			_processing = false;
		}

		private void CancelButtonClicked_CreateEducation()
		{
			addEducation = false;
			createEducation = new CreateEducationDetailCommand();
		}

		private async void CancelButtonClicked_UpdateEducation()
		{
			editingCardId = Guid.NewGuid();
			await GetEducations();
			StateHasChanged();
		}

		private async Task UpdateEducationHandle()
		{
			_processing = true;
			try
			{
				var result = await UserProfileService.UpdateEducationAsync(educationUpdate);
				if (result.IsSuccessStatusCode)
				{
					editingCardId = Guid.NewGuid();
					Snackbar.Add("Update Education successfully.", MudBlazor.Severity.Success);

					await GetEducations();
					StateHasChanged();
				}
				else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
				{
					await BadRequestResponse(result);
				}
			}
			catch (HttpRequestException)
			{
				ServerNotResponding();
			}

			_processing = false;
		}

		#endregion


	}
}
