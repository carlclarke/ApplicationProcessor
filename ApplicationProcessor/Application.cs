using System;
using System.Text;
using ULaw.ApplicationProcessor.Enums;

/*
I chose to add comments rather than change the code because I felt I could cover more issues in the allocated 1 hour.

I have been looking for good opportunites for DI however most of the dependency is on .NET, a real situation would
present more opportunities for DI

Without discarding how this currently works too much I would create a IApplicant interface with an abstract Process 
method and the faculty, courseCode, startdate, title, firstName, string lastName, dateOfBirth, requiresVisa, etc, 
properties. Each type of applicant is dictated by the DegreeGrade & DegreeSubject and so a number of classes need
to be created to implement IApplicant for the current (and future) combinations. This will provide SOLID OCP. The
current Application.Process() method would then call a Factory Method which would contruct the correct class and 
then call the IApplicant.Process() implementation on it and retuen the result. In a production system this
approach would mean that the Process method, as written below, would not get disturbed every time a new email was
required, of course something has to change to satisfy a new requirement but the change should be limited to
the Factory Method and the creation of the new Applicant class.

I hope that you can understand the above without code examples, I would love to write it but I think it would take
more than one hour to review this project and re-write it
 
 */

namespace ULaw.ApplicationProcessor
{
    //CC: Add interface IApplication with abstract method 'Process'
    public class Application
    {
        //CC: I would consider changing the lengthy parameter list to be a class
        //CC: There are mixed case parameters, typically they would be camel case
        public Application(string faculty, string CourseCode, DateTime Startdate, string Title, string FirstName, string LastName, DateTime DateOfBirth, bool requiresVisa)
        {
            this.ApplicationId = new Guid();
            this.Faculty = faculty;
            this.CourseCode = CourseCode;
            this.StartDate = Startdate;
            this.Title = Title;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.RequiresVisa = RequiresVisa;   //CC: this is a bug, the parameter value is not used
            this.DateOfBirth = DateOfBirth;
        }

        //CC: In this use none of these instance variables need to be 'public'
        public Guid ApplicationId { get; private set; }
        public string Faculty { get; private set; }
        public string CourseCode { get; private set; }
        public DateTime StartDate { get; private set; }
        public string Title { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public bool RequiresVisa { get; private set; }

        public DegreeGradeEnum DegreeGrade { get; set; }
        public DegreeSubjectEnum DegreeSubject { get; set; }

        public string Process()
        {
            //CC: I would consider using HtmlTextWriter rather than string literals for the tags.
            //CC: HtmlTextWriter has a WriteEncodedText method to perform encoding of characters such as '<>&'
            //CC: I find string interpolation easier to read and understand than using string.Format()
            //CC: I would try to keep the length of the string literals down to something ending around col 120,
            //    the longest ends on col 304. Use of verbatim strings or string concatination - just to
            //    make the source code easier to read
            var result = new StringBuilder("<html><body><h1>Your Recent Application from the University of Law</h1>");

            //CC: Why does every line have a space after the tag?
            if (DegreeGrade == DegreeGradeEnum.twoTwo)
            {
                result.Append(string.Format("<p> Dear {0}, </p>", FirstName));
                result.Append(string.Format("<p/> Further to your recent application for our course reference: {0} starting on {1}, we are writing to inform you that we are currently assessing your information and will be in touch shortly.", this.CourseCode, this.StartDate.ToLongDateString()));
                result.Append("<br/> If you wish to discuss any aspect of your application, please contact us at AdmissionsTeam@Ulaw.co.uk.");
                result.Append("<br/> Yours sincerely,");
                result.Append("<p/> The Admissions Team,");
            }
            else
            {
                if (DegreeGrade == DegreeGradeEnum.third)
                {
                    result.Append(string.Format("<p> Dear {0}, </p>", FirstName));
                    result.Append("<p/> Further to your recent application, we are sorry to inform you that you have not been successful on this occasion.");
                    result.Append("<br/> If you wish to discuss the decision further, or discuss the possibility of applying for an alternative course with us, please contact us at AdmissionsTeam@Ulaw.co.uk.");
                    result.Append("<br> Yours sincerely,");
                    result.Append("<p/> The Admissions Team,");
                }
                else
                {
                    if (DegreeSubject == DegreeSubjectEnum.law || DegreeSubject == DegreeSubjectEnum.lawAndBusiness)
                    {
                        decimal depositAmount = 350.00M;

                        result.Append(string.Format("<p> Dear {0}, </p>", FirstName));
                        result.Append(string.Format("<p/> Further to your recent application, we are delighted to offer you a place on our course reference: {0} starting on {1}.", this.CourseCode, this.StartDate.ToLongDateString()));
                        result.Append(string.Format("<br/> This offer will be subject to evidence of your qualifying {0} degree at grade: {1}.", DegreeSubject.ToDescription(), DegreeGrade.ToDescription()));
                        result.Append(string.Format("<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £{0} deposit fee to secure your place.", depositAmount.ToString()));
                        result.Append(string.Format("<br/> We look forward to welcoming you to the University,"));
                        result.Append(string.Format("<br/> Yours sincerely,"));
                        result.Append(string.Format("<p/> The Admissions Team,"));
                    }
                    else
                    {
                        result.Append(string.Format("<p> Dear {0}, </p>", FirstName));
                        result.Append(string.Format("<p/> Further to your recent application for our course reference: {0} starting on {1}, we are writing to inform you that we are currently assessing your information and will be in touch shortly.", this.CourseCode, this.StartDate.ToLongDateString()));
                        result.Append("<br/> If you wish to discuss any aspect of your application, please contact us at AdmissionsTeam@Ulaw.co.uk.");
                        result.Append("<br/> Yours sincerely,");
                        result.Append("<p/> The Admissions Team,");
                    }
                }
            }

             result.Append(string.Format("</body></html>"));

            return result.ToString();
        }

    }
}

