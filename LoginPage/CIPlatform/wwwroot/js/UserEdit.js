
$(document).ready(function () {
    var userImgSrc = "";

    $("#user-edit-formtag").submit(function (e) {
        e.preventDefault();
        var name = $("#useredit-name").val();
        var surname = $("#useredit-surname").val();
        var eid = $("#useredit-eid").val();
        var manager = $("#useredit-manager").val();
        var title = $("#useredit-title").val();
        var dept = $("#useredit-dept").val();
        var myprofile = $("#useredit-myprofile").val();
        var whyIVol = $("#useredit-whyIVol").val();
        var cityId = $("#user-city option:selected").attr("data-cityid");
        var CountryId = $("#user-country option:selected").attr("data-countryid");
        var userAvailabillity = $("#user-availability option:selected").attr("data-availabilityid");
        var userLinkedin = $("#user-linkedin").val();
        var avatar = userImgSrc;

        var skillSpans = $('.user-skill-info-form span');
        // Create an empty array to store the skill ids
        var skillIds = [];
        // Loop over each span element and extract the data-skillid attribute value
        skillSpans.each(function (index) {
            var skillId = $(this).data('skillid');
            skillIds.push(skillId);
        });
        console.log(skillIds);

        $.ajax({
            type: "POST",
            url: "/User/SaveEditUser",
            data: {
                name: name,
                surname: surname,
                eid: eid,
                manager: manager,
                title: title,
                dept: dept,
                myprofile: myprofile,
                whyIVol: whyIVol,
                cityId: cityId,
                CountryId: CountryId,
                userAvailabillity: userAvailabillity,
                userLinkedin: userLinkedin,
                skillIds: skillIds,
                avatar : avatar
            },
            success: function (result) {
                console.log("Code executed successfully");
                if (result.statusCode == 200) {
                    toastr.success(result.message);
                } else {
                    toastr.error(result.message);
                }

                $.ajax({
                    url: '/User/GetHeader',
                    success: function (result) {
                        $('#user-header').html(result);
                    }
                });

            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });

    });

    $("#user-country").on('change', function () {
        var countryId = $(this).val();
        $.ajax({
            url: "/User/GetCities",
            type: "POST",
            data: { countryId: countryId },
            success: function (data) {
                var cityDropdown = $("#user-city");
                cityDropdown.empty();
                $.each(data, function (i, city) {
                    cityDropdown.append($("<option/>").val(city.cityId).text(city.name).attr("data-cityid", city.cityId));
                });
            },
            error: function () {
                alert("Error occurred while getting cities.");
            }
        });
    });

    const confirmPwdInput = $('#cnewpwd');
    confirmPwdInput.on('input', validatePassword);

    $("#userEditPasswordForm").submit(function (e) {
        e.preventDefault();
        var oldPassword = $("#oldpwd").val();
        var newPassword = $("#newpwd").val();

        $.ajax({
            type: "POST",
            url: "/User/ChangePassword",
            data: {
                oldPassword: oldPassword,
                newPassword : newPassword
            },
            success: function (result) {
                console.log(result);
                if (result.statusCode === 200) {
                    $(".btn-close").click();
                    toastr.success(result.message);
                } else {
                    toastr.error(result.message);
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });

    });

    $('#toRightSkills').on('click', function () {
        $(".selected-skills-list").html("");
        var LeftSelectedSkills = document.querySelectorAll(".left-skill-check:checked");
        var userSkills = [];
        for (var skill of LeftSelectedSkills) {
            var UserSkillItem = {
                SkillId: parseInt(skill.id.toString().split('-')[1]),
                SkillName: skill.nextElementSibling.innerHTML
            };
            userSkills.push(UserSkillItem);
            $(".selected-skills-list").append(`
                                <div class="form-check">
                                    <input class="btn-check form-check-input right-skill-check" type="checkbox" value="${UserSkillItem.SkillId}" id="skillcheckboxright-${UserSkillItem.SkillId}">
                                    <label class="form-check-label" for="skillcheckboxright-${UserSkillItem.SkillId}">
                                        ${UserSkillItem.SkillName}
                                    </label>
                                </div>`);
        }
    });
 
    $('#toLeftSkills').on('click', function () {
        var RightSelectedSkills = document.querySelectorAll(".right-skill-check:checked");
        for (var item of RightSelectedSkills) {
            var SkillId = "skillcheckbox-" + item.id.toString().split('right-')[1];
            document.getElementById(SkillId).checked = false;
            document.getElementById(item.id).nextElementSibling.remove();
            document.getElementById(SkillId).parentElement.classList.remove('bg-secondary');
            document.getElementById(item.id).parentElement.remove();
        }
    });

    $("#addskills-save-useredit").on('click', function () {
        var RightSelectedSkills = document.querySelectorAll(".left-skill-check:checked");
        var htmlToDiv = "";
        for (var item of RightSelectedSkills) {
            var key = item.id.toString().split('-')[1];
            var name = item.nextElementSibling.textContent.trim();
            htmlToDiv += `<span data-skillid="${key}" class="me-3">${name}</span>`
        }
        $("#skillDivInUserEdit").html(htmlToDiv);
    });



    //for checkbox background in skills
    $('input[type="checkbox"]:checked').each(function () {
        $(this).parent('.form-check').addClass('bg-secondary');
    });
    
    //$(".left-skill-check,.right-skill-check").on('change' ,function () {
    //    $(this).parent('.form-check').toggleClass('bg-secondary');
    //});
    $(".checkbox-list").on('change', '.left-skill-check', function () {
        $(this).parent('.form-check').toggleClass('bg-secondary');
    });
    $(".checkbox-list").on('change', '.right-skill-check', function () {
        $(this).parent('.form-check').toggleClass('bg-secondary');
    });

    //for validating password
    function validatePassword() {
        console.log("Validate pwd");
        const newPwdInput = document.getElementById('newpwd');
        const newPwdValue = newPwdInput.value;
        const confirmPwdValue = this.value;
        const errorText = 'New password and confirm password must be the same';

        if (newPwdValue === confirmPwdValue) {
            // Passwords match, enable "Change Password" button
            document.getElementById('changePasswordButton').disabled = false;
            // Clear error message if it was shown previously
            this.setCustomValidity('');
        } else {
            // Passwords don't match, disable "Change Password" button and show error message
            document.getElementById('changePasswordButton').disabled = true;
            this.setCustomValidity(errorText);
        }
    }

    //for user image change
    try {
        const avatarUpload = document.querySelector('#avatar-upload');
        const userImage = document.querySelector('.user-img-left');

        avatarUpload.addEventListener('change', function () {
            const file = avatarUpload.files[0];
            const reader = new FileReader();

            reader.addEventListener('load', function () {
                userImage.src = reader.result;
                userImgSrc = userImage.src;
            });

            if (file) {
                reader.readAsDataURL(file);
            }
        });

        userImage.addEventListener('click', function (e) {
            e.preventDefault();
            avatarUpload.click();
        });
    } catch (error) {
        console.error(error);
    }

});



function clearPasswordFields() {
    $("#oldpwd").val("");
    $("#newpwd").val("");
    $("#cnewpwd").val("");
}