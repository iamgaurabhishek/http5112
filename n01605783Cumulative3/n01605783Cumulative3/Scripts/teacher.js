function ValidateTeacher() {

	var IsValid = true;
	var ErrorMsg = "";
	var ErrorBox = document.getElementById("ErrorBox");
	var TeacherFirstName = document.getElementById('TeacherFname').value;
	var TeacherLastName = document.getElementById('TeacherLname').value;
	var TeacherHireDate = document.getElementById('TeacherHireDate').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;
	var TeacherEmployeeNumber = document.getElementById('TeacherEmployeeNumber').value;

	//First Name is two or more characters
	if (TeacherFirstName == "") {
		IsValid = false;
		ErrorMsg += "First Name is required.<br>";
	}
	//Last Name is two or more characters
	if (TeacherLastName == "") {
		IsValid = false;
		ErrorMsg += "Last Name is required.<br>";
	}
	// Hire Date validation
	if (TeacherHireDate == undefined || TeacherHireDate == "" || TeacherHireDate == null) {
		IsValid = false;
		ErrorMsg += "Hire Dateis required.<br>";
	}
	// Teacher Salary validation
	if (TeacherSalary == "") {
		IsValid = false;
		ErrorMsg += "Teacher Salaryis required.<br>";
	}
	//Employee number is valid or not
	if (TeacherEmployeeNumber == "") {
		IsValid = false;
		ErrorMsg += "Employee number is required.	<br>";
	}
	//
	if (!IsValid) {
		ErrorBox.style.display = "block";
		ErrorBox.innerHTML = ErrorMsg;
	} else {
		ErrorBox.style.display = "none";
		ErrorBox.innerHTML = "";
	}


	return IsValid;
}
function AddTeacher() {

	//check for validation straight away
	var IsValid = ValidateTeacher();
	if (!IsValid) return;

	//goal: send a request which looks like this:
	//POST : http://localhost:51326/api/TeacherData/AddTeacher
	//with POST data of authorname, bio, email, etc.

	var URL = "https://localhost:44387/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFirstName = document.getElementById('TeacherFname').value;
	var TeacherLastName = document.getElementById('TeacherLname').value;
	var TeacherHireDate = document.getElementById('TeacherHireDate').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;
	var TeacherEmployeeNumber = document.getElementById('TeacherEmployeeNumber').value;

	var TeacherData = {
		"TeacherFirstName": TeacherFirstName,
		"TeacherLastName": TeacherLastName,
		"TeacherHireDate": TeacherHireDate,
		"TeacherSalary": TeacherSalary,
		"TeacherEmployeeNumber": TeacherEmployeeNumber
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}



function UpdateTeacher(TeacherId) {

	//check for validation straight away
	var IsValid = ValidateTeacher();
	if (!IsValid) return;

	//goal: send a request which looks like this:
	//POST : http://localhost:51326/api/TeacherData/UpdateTeacher/{id}
	//with POST data of authorname, bio, email, etc.

	var URL = "https://localhost:44387/api/TeacherData/UpdateTeacher/" + TeacherId;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFirstName = document.getElementById('TeacherFname').value;
	var TeacherLastName = document.getElementById('TeacherLname').value;
	var TeacherHireDate = document.getElementById('TeacherHireDate').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;
	var TeacherEmployeeNumber = document.getElementById('TeacherEmployeeNumber').value;



	var TeacherData = {
		"TeacherFirstName": TeacherFirstName,
		"TeacherLastName": TeacherLastName,
		"TeacherHireDate": TeacherHireDate,
		"TeacherSalary": TeacherSalary,
		"TeacherEmployeeNumber": TeacherEmployeeNumber
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}