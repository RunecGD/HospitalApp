using System.Text;
using System.Xml.Serialization;
using HospitalApp.BusinessLayer;

namespace HospitallApp.ServiceLayer;

public class EpicrisisService
{
    
    public string SerializeToXml(Epicrisis epicrisis)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Epicrisis));
        using (StringWriter stringWriter = new StringWriter())
        {
            serializer.Serialize(stringWriter, epicrisis);
            return stringWriter.ToString();
        }
    }

    public void SaveToFile(string xmlString, string filePath)
    {
        File.WriteAllText(filePath, xmlString, Encoding.UTF8);
    }
}