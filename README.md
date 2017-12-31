# PdfFormTestTask
REST service for PDF Form

# Explanatory note

Task:
	Create a REST service for working with the fields of a PDF document form.
	The service should allow:

		1. Upload PDF document.
		2. Get the list of form fields.
		3. Get the values of the fields.
		4. Fill in the fields.
		5. Save the completed fields.
		6. The service should allow access only to registered users (the list of users can be stored in an arbitrary form).

	Using the developed service, implement a visual service for downloading a PDF document, displaying its fields with the ability to fill them and saving the changes made to the PDF document.
	The service should be implemented under .NET in C # using the Aspose.Pdf library.

Choice of technologies:

	The Web Api framework is taken as the best solution for a simple REST service on ASP.NET.
	Web Api framework + MVC as base.
	.Net 4.6.1
	Bootstrap HTML/CSS
	jQuery
	Git

Solution structure:

	The solution consists of four projects.
	1. PdfFormTestTask.Service contains the service and the web application that uses it.
	2. PdfFormTestTask.Model contains a data model for the REST service, as well as data source for the service.
		The source of the data is the singleton class PfsRepository.
	3. PdfFormTestTask.Client contains the server client implementation for the REST service.
	4. PdfFormTestTask.Tests contains Unit tests for some classes. (MS Test)

Web application contains 3 pages.

	1.Login page
		There are two users in the repositary.
		user1 pass1
		user2 pass2
	
	2. Forms page
		Pdf forms can be uploaded, downloaded and selected for edit on this page.	

	3. PDF Form Fields
		The page allows you to edit the values of the fields.
		Supported fields: Textbox, Radio Buttons List, Checkbox, Combobox implemented as HTML select.


			