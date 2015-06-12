using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserPublicCenter.AirTickets.history
{
    public class order
    {
        public order()
        { }
        #region Model
        private int? _id;
        private string _state;
        private string _type;
        private string _clearcode;
        private string _number;
        private string _clearcodenumber;
        private string _flightnumber;
        private DateTime _tickettime;
        private DateTime _paytime;
        private DateTime _flighttime;
        private string _orderpeople;
        private string _carrier;
        private string _discount;
        private string _passenger;
        private string _voyage;
        private string _pnr;
        private string _ticketprice;
        private string _taxprice;
        private string _ranyoufee;
        private string _jijianfee;
        private string _rebate;
        private string _shouxufee;
        private string _yongjin;
        private string _tgqfee;
        private string _payfee;
        private string _actualpayprice;
        private string _buyer;
        private string _buyername;
        private string _buyeropuserid;
        private string _supplier;
        private string _policy;
        private string _payway;
        /// <summary>
        /// 
        /// </summary>
        public int? ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string state
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string clearCode
        {
            set { _clearcode = value; }
            get { return _clearcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string clearCodeNumber
        {
            set { _clearcodenumber = value; }
            get { return _clearcodenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string flightNumber
        {
            set { _flightnumber = value; }
            get { return _flightnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ticketTime
        {
            set { _tickettime = value; }
            get { return _tickettime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime payTime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime flightTime
        {
            set { _flighttime = value; }
            get { return _flighttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string orderPeople
        {
            set { _orderpeople = value; }
            get { return _orderpeople; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Carrier
        {
            set { _carrier = value; }
            get { return _carrier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Passenger
        {
            set { _passenger = value; }
            get { return _passenger; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Voyage
        {
            set { _voyage = value; }
            get { return _voyage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PNR
        {
            set { _pnr = value; }
            get { return _pnr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ticketPrice
        {
            set { _ticketprice = value; }
            get { return _ticketprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string taxPrice
        {
            set { _taxprice = value; }
            get { return _taxprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ranyouFee
        {
            set { _ranyoufee = value; }
            get { return _ranyoufee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string jijianFee
        {
            set { _jijianfee = value; }
            get { return _jijianfee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string rebate
        {
            set { _rebate = value; }
            get { return _rebate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string shouxuFee
        {
            set { _shouxufee = value; }
            get { return _shouxufee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string yongjin
        {
            set { _yongjin = value; }
            get { return _yongjin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tgqFee
        {
            set { _tgqfee = value; }
            get { return _tgqfee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string payFee
        {
            set { _payfee = value; }
            get { return _payfee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string actualPayPrice
        {
            set { _actualpayprice = value; }
            get { return _actualpayprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string buyer
        {
            set { _buyer = value; }
            get { return _buyer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string buyername
        {
            set { _buyername = value; }
            get { return _buyername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string buyerOPUserId
        {
            set { _buyeropuserid = value; }
            get { return _buyeropuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Policy
        {
            set { _policy = value; }
            get { return _policy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string payWay
        {
            set { _payway = value; }
            get { return _payway; }
        }
        #endregion Model
    }
}
