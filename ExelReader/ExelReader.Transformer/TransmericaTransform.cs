using ExcelDataReader;
using ExelReader.Transformer.FileTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExelReader.Transformer.DateProvider;

namespace ExelReader.Transformer
{
    public class TransmericaTransform : Serilization
    {

        public TransmericaTransform(string from, string to)
            : base(from, to)
        {

        }
        public override void transform()
        {
            try
            {
                var fileNames = Directory.GetFiles(DirectoryFrom, "*", SearchOption.TopDirectoryOnly).Where(x => !x.Contains("\\AUM")).ToArray();
             
                foreach (var file in fileNames)
                {
                    using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
                    {
                        using (var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                        {
                            var dataSet = excelDataReader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (x) => new ExcelDataTableConfiguration { UseHeaderRow = true }
                            });
                            List<BrokerInfo> brokerInfos = PopulateBrokerInfo(dataSet.Tables["BrokerInfo"]);
                            List<ContactInfo> contactInfos = PopulateContactInfo(dataSet.Tables["ContractInfo"]);
                            List<FundInfo> fundInfos = PopulateFundInfo(dataSet.Tables["FundInfo"]);

                            this.dateProvider.Advisors = brokerInfos.Select(x => new Advisor
                            {
                                ZipCode = contactInfos.Where(c => c.sub_id == x.sub_id && c.contract_id == x.contract_id).FirstOrDefault().zip ?? "NOT SPECIFIED",
                                FirstName = x.first_name,
                                LastName = x.last_name,
                                Address1 = contactInfos.Where(c => c.sub_id == x.sub_id && c.contract_id == x.contract_id).FirstOrDefault().street1 ?? "NOT SPECIFIED",
                                Address2 = contactInfos.Where(c => c.sub_id == x.sub_id && c.contract_id == x.contract_id).FirstOrDefault().street2 ?? "NOT SPECIFIED",
                                City = contactInfos.Where(c => c.sub_id == x.sub_id && c.contract_id == x.contract_id).FirstOrDefault().city ?? "NOT SPECIFIED"



                            }).ToList();

                        };
                    };
                    this.Serilize();
                }
            }

            catch (Exception)
            {

                throw;
            }
        }

        private List<FundInfo> PopulateFundInfo(DataTable dataTable)
        {
            List<FundInfo> funds = new List<FundInfo>();
            foreach (DataRow row in dataTable.Rows)
            {
                string ticker = row["ticker"].ToString().Trim();
                string cusip = row["cusip"].ToString().Trim();
                string fund_name = row["fund_name"].ToString().Trim();
                string secid = row["secid"].ToString().Trim();

                funds.Add(new FundInfo
                {
                    contract_id = row["contract_id"].ToString().Trim(),
                    sub_id = row["sub_id"].ToString().Trim(),
                    ticker = ticker,
                    cusip = cusip,
                    fund_name = fund_name,
                    amount = float.Parse(row["amount"].ToString().Trim()),
                    secid = row["secid"].ToString().Trim()
                });
            }
            return funds;

        }

        private List<ContactInfo> PopulateContactInfo(DataTable dataTable)
        {
            List<ContactInfo> contracts = new List<ContactInfo>();
            foreach (DataRow row in dataTable.Rows)
            {
                // Take all plans and assgin them to 401(k)
                if (!row["contract_status"].ToString().Trim().Contains("Discontinued (Pending)"))
                {
                    contracts.Add(new ContactInfo
                    {
                        as_Of_Date = DateTime.Parse(row["As_Of_Date"].ToString().Trim(), new System.Globalization.CultureInfo("en-US")),
                        alliance_name = row["alliance_name"].ToString().Trim(),
                        contract_id = row["contract_id"].ToString().Trim(),
                        sub_id = row["sub_id"].ToString().Trim(),
                        client_name = row["client_name"].ToString().Trim(),
                        contract_status = row["contract_status"].ToString().Trim(),
                        plan_type = row["plan_type"].ToString().Trim(),
                        last_name = row["last_name"].ToString().Trim(),
                        first_name = row["first_name"].ToString().Trim(),
                        street1 = row["street1"].ToString().Trim(),
                        street2 = row["street2"].ToString().Trim(),
                        city = row["city"].ToString().Trim(),
                        state = row["state"].ToString().Trim(),
                        zip = row["zip"].ToString().Trim()
                    });
                }
            }
            return contracts;
        }

        private List<BrokerInfo> PopulateBrokerInfo(DataTable dataTable)
        {
            List<BrokerInfo> brokers = new List<BrokerInfo>();
            BrokerInfo broker;
            foreach (DataRow row in dataTable.Rows)
            {
                // Get only the first broker, if there are more ignore the rest for the same contract
                broker = brokers.FirstOrDefault(x => x.contract_id == row["contract_id"].ToString().Trim() && x.sub_id == row["sub_id"].ToString().Trim());
                if (broker == null && !String.IsNullOrEmpty(row["ID"].ToString().Trim()))
                {
                    brokers.Add(new BrokerInfo
                    {
                        contract_id = row["contract_id"].ToString().Trim(),
                        sub_id = row["sub_id"].ToString().Trim(),
                        ID = row["ID"].ToString().Trim(),
                        last_name = row["last_name"].ToString().Trim(),
                        first_name = row["first_name"].ToString().Trim()
                    });
                }
            }
            return brokers;
        }
    }
}
