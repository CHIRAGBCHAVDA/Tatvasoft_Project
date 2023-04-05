$(document).ready(function () {
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
        var cityId = $("#user-city").val();
        var CountryId = $("#user-country option:selected").attr("data-countryid");
        var userAvailabillity = $("#user-availability option:selected").attr("data-availabilityid");
        var userLinkedin = $("#user-linkedin").val();

        var skillSpans = $('.user-skill-info-form span');
        // Create an empty array to store the skill ids
        var skillIds = [];
        // Loop over each span element and extract the data-skillid attribute value
        skillSpans.each(function (index) {
            var skillId = $(this).data('skillid');
            skillIds.push(skillId);
        });

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
                skillIds: skillIds
            },
            success: function (result) {
                console.log("Code executed successfully");
            },
            error: function (xhr, status, error) {
                console.log(error);
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
            debugger;
            var SkillId = "skillcheckbox-" + item.id.toString().split('-')[1];
            document.getElementById(SkillId).checked = false;
            document.getElementById(item.id).nextElementSibling.remove();
            document.getElementById(item.id).remove();
        }
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
});



function clearPasswordFields() {
    $("#oldpwd").val("");
    $("#newpwd").val("");
    $("#cnewpwd").val("");
}