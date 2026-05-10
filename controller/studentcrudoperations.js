var studentcrudoperation = {
    init: function() {
        $("#btnid").click(function() {
            studentcrudoperation.student()

        })

        $("#detailsid").click(function () {
            studentcrudoperation.getdetail()
        })
        $("#updateid").click(function () {
            studentcrudoperation.updatedetaill()
        })
    },
    student: function() {
        var name = $("#nametxt").val();
        var email = $("#txt_Email").val();
        var cours = $("#txt_cours").val();
        var phone = $("#txt_phone").val();
        var password = $("#psw_password").val();
        var cnfpass = $("#psw_cnfpassword").val();
        if (password != cnfpass) {
            $("#spnname").text("password does not match");
        }
        else {
            $("#spnname").text("");
            var postdata = {
                Name : name,
                Email : email,
                Course : cours,
                phonenumber : phone,
                Password : password
            };
            $.ajax({
                type: "POST",
                url: "Student",
                data: postdata,
                success: function(result) {
                    alert(result);
                    $("#nametxt").val("");
                    $("#txt_Email").val("");
                    $("#txt_cours").val("");
                    $("#txt_phone").val("");
                    $("#psw_password").val("");
                    $("#psw_cnfpassword").val("");
                    studentcrudoperation.getdetail()
                }
            });
        }

    },
    getdetail: function () {
        $("#tbbody").empty()
        $.ajax({
            type: "Get",
            url: "Getdetails",
            success: function (result) {
                debugger
                var list = JSON.parse(result);
                var html = "";
                for (var i = 0; i < list.length; i++) {
                    html += '<tr><td>' + list[i].Name + '</td><td>' + list[i].Email + '</td><td>' + list[i].Course + '</td><td>' + list[i].phonenumber + '</td><td><button onclick = "studentcrudoperation.editstud(' + list[i].Id + ')" > Edit</button ></td ><td><button onclick = "studentcrudoperation.deletestd(' + list[i].Id +')" > Delete</button ></td ></tr>';
                }
                $("#tbbody").append(html);

            }
        })
    },
    editstud: function (Id) {
        $.ajax({
            type: "Get",
            url: "Editstudent",
            data: { 'Id': Id },
            success: function (result) {
                var data = JSON.parse(result);
                
                $("#nametxt").val(data.Name);
                $("#idlabl").val(data.Id);
                $("#txt_Email").val(data.Email);
                $("#txt_cours").val(data.Course);
                $("#txt_phone").val(data.phonenumber);
            }

        })
        
   
    },
    updatedetaill: function () {
        var name = $("#nametxt").val();
        var email = $("#txt_Email").val();
        var cours = $("#txt_cours").val();
        var phone = $("#txt_phone").val();
        var password = $("#psw_password").val();
        var cnfpass = $("#psw_cnfpassword").val();
        if (password != cnfpass) {
            $("#spnname").text("password does not match");
        }
        else {
            $("#spnname").text("");
            var postdata = {
                Id: $("#idlabl").val(),
                Name: name,
                Email: email,
                Course: cours,
                phonenumber: phone,
                Password: password
            };
            $.ajax({
                type: "POST",
                url: "Updatedetails",
                data: postdata,
                success: function (result) {
                    debugger
                    alert(result);
                    $("#nametxt").val("");
                    $("#txt_Email").val("");
                    $("#txt_cours").val("");
                    $("#idlabl").val(0);
                    $("#txt_phone").val("");
                   
                    studentcrudoperation.getdetail()
                }
            });
        }

    },
    deletestd: function (Id) {
        $.ajax({
            type: "Get",
            url: "Deletestudent",
            data: { 'Id': Id },
            success: function (result) {
                alert(result)
                studentcrudoperation.getdetail()
            }
        })

    }
    

}
