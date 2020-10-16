using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Reflection;
//using Aspose.BarCode.Generation;

namespace BonusSystem
{
    public class ViewModel : ViewModelBase
    {
        public ObservableCollection<string> ConfigProfiles { get; private set; }
        Profiles[] Profiles = new Profiles[] { };

        private string _processingAddress = String.Empty;
        private string _cardNum = "8000010011037034";
        private string _cardNumForHashEP = "8000010011798024";
        private string _cardNumForActivation = "8000010011037034";
        private string _phoneMobile = "79210000000";
        private string _textMessage = "Тестовое сообщение BMS Group";
        private string _terminalIdForActivationBMS = String.Empty;
        private string _accountType = "GLOBAL";
        private string _cardNumForLockUnlock = "8000010011037034";
        private string _hashCardEP = String.Empty;
        private string _receiptData = String.Empty;
        private string _rrn = String.Empty;
        private string _cashBackRrn = String.Empty;
        private string _cashBackFile = "cash_back.jsn";
        private string _token = "12345";
        private string _linkcardNum = "8000010012345678";
        private string _referFriendCard = "8000010012345678";
        private string _billFile = "bill.jsn";
        private string _hashCardNum = String.Empty;
        private string _terminalId = String.Empty;
        private string _serverAddr = String.Empty;
        private string _serverAltAddr = String.Empty;
        private string _terminalPassword = String.Empty;
        private string _sslKey = String.Empty;
        private string _retailNetworkName = String.Empty;
        private string _retailPointAddress = String.Empty;
        private string _receiptHeader = String.Empty;
        private string _receiptMainHeader = String.Empty;
        private string _receiptOrganizerFee = String.Empty;
        private string _receiptFooter = String.Empty;
        private string _protocolVersion = String.Empty;
        private string _billXMLConvertTable = "UTF8";
        private string _beginDate = String.Format("{0:yyyy-MM-dd}", DateTime.Now) + "T09:00:00";
        private string _endDate = String.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1)) + "T09:00:00";
        private string _reportData = String.Empty;
        private string dataSource = String.Empty;
        private string cashBackFileTemp = String.Empty;

        private string _QRImage;
        private string _title = "cbposlib1 v." + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        
        private int _selectItem = 0;
        private int _sslMode = 1;
        private int _hashCardNumConfig = 0;
        private int _shiftMode = 0;
        private int _sentSpendOnActivation = 0;
        private int _logSize = 10000;
        private int _timeZone = 3;

        private int _globalBonusOffline_1 = 10;
        private int _localBonusOffline_1 = 0;
        private int _globalBonusOffline_2 = 20;
        private int _localBonusOffline_2 = 0;
        private int _globalBonusOffline_5 = 30;
        private int _localBonusOffline_5 = 0;
        private int _globalBonusOffline_6 = 0;
        private int _localBonusOffline_6 = 0;

        private int _noLocalBonusPrint = 1;
        private int _noDiscountBonusPrint = 1;
        private int _noGlobalBonusPrint = 0;
        private int _timeout = 40;
        private int _connect_timeout = 15;
        private int _retry_pause = 5;
        private int _offlineMode = 1;
        private int _orgFeeOffline = 0;

        private double _depositSum = 100.00;
        private double _giftSum = 100.00;
        private double _cashBackSum = 0.00;
        private double _activationAmount = 0.00;

        private bool _isEnableExtraPayment = true;
        private bool _isSecondTypeCashBack = true;
        private bool _isPartial = true;
        private bool _isPartialEnabled = false;
        private bool _isCashBackFileEnabled = false;
        private bool _isCashBackSumEnabled = true;
        private bool _isSimpleActivation = true;
        private bool _isGiftCard = false;
        private bool _isReferAFriend = false;
        private bool _isWithoutConfirm = false;
        private bool _isRecreateBill = true;
        private bool _isTokenNeed = true;
        private bool _isEnabledUpdate = true;

        private ICommand _OpenShiftOperation;
        private ICommand _CloseShiftOperation;
        private ICommand _SendSMSOperation;
        private ICommand _SendPushOperation;
        private ICommand _Update;
        private ICommand _RefreshActionsOperation;
        private ICommand _BMSConfirmOperation;
        private ICommand _BMSCancelOperation;
        private ICommand _BMSActivationOperation;
        private ICommand _BMSLockCardOperation;
        private ICommand _BMSUnLockCardOperation;
        private ICommand _BMSSendTokenOperation;
        private ICommand _balanceOperation;
        private ICommand _depositOperation;
        private ICommand _cancelOperation;
        private ICommand _paymentCancelOperation;
        private ICommand _exchangeOperation;
        private ICommand _linkCardOperation;
        private ICommand _cashBackOperation;
        private ICommand _activationOperation;
        private ICommand _earnOperation;
        private ICommand _spendOperation;
        private ICommand _confirmOperation;
        private ICommand _hashCardNumOperation;
        private ICommand _saveConfig;
        private ICommand _sendOfflineOperation;
        private ICommand _summaryReportOperation;
        private ICommand _detailReportOperation;
        private ICommand _hashCardEPOperation;
        private ICommand _hashCardFindTime;
        private ICommand _cardNumFromHashOperation;

        Config Config = new Config();

        public bool isEnableExtraPayment
        {
            get
            {
                return _isEnableExtraPayment;
            }
            set
            {
                _isEnableExtraPayment = value;
                RaisePropertyChanged(() => isEnableExtraPayment);
            }
        }
        
        public bool isTokenNeed
        {
            get
            {
                return _isTokenNeed;
            }
            set
            {
                _isTokenNeed = value;
                RaisePropertyChanged(() => isTokenNeed);
            }
        }
        
        public bool isCashBackSumEnabled
        {
            get
            {
                return _isCashBackSumEnabled;
            }
            set
            {
                _isCashBackSumEnabled = value;
                RaisePropertyChanged(() => isCashBackSumEnabled);
            }
        }
        
        public bool isCashBackFileEnabled
        {
            get
            {
                return _isCashBackFileEnabled;
            }
            set
            {
                _isCashBackFileEnabled = value;
                RaisePropertyChanged(() => isCashBackFileEnabled);
            }
        }
        
        public bool isPartialEnabled
        {
            get
            {
                return _isPartialEnabled;
            }
            set
            {
                _isPartialEnabled = value;
                RaisePropertyChanged(() => isPartialEnabled);
            }
        }
        
        public bool isSecondTypeCashBack
        {
            get
            {
                return _isSecondTypeCashBack;
            }
            set
            {
                _isSecondTypeCashBack = value;

                if (_isSecondTypeCashBack)
                {
                    isPartialEnabled = false;
                    isCashBackFileEnabled = false;
                    isCashBackSumEnabled = true;
                }
                else
                {
                    isPartialEnabled = true;
                    isCashBackFileEnabled = true;
                    isCashBackSumEnabled = false;
                }

                RaisePropertyChanged(() => isSecondTypeCashBack);
            }
        }
      
        public bool isWithoutConfirm
        {
            get
            {
                return _isWithoutConfirm;
            }
            set
            {
                _isWithoutConfirm = value;
                RaisePropertyChanged(() => isWithoutConfirm);
            }
        }

        public bool isSimpleActivation
        {
            get
            {
                return _isSimpleActivation;
            }
            set
            {
                _isSimpleActivation = value;

                if(_isSimpleActivation)
                {
                    isGiftCard = false;
                    isReferAFriend = false;
                }

                RaisePropertyChanged(() => isSimpleActivation);
            }
        }

        public bool isGiftCard
        {
            get
            {
                return _isGiftCard;
            }
            set
            {
                _isGiftCard = value;

                if(_isGiftCard)
                {
                    isSimpleActivation = false;
                    isReferAFriend = false;
                }
                
                RaisePropertyChanged(() => isGiftCard);
            }
        }

        public bool isReferAFriend
        {
            get
            {
                return _isReferAFriend;
            }
            set
            {
                _isReferAFriend = value;

                if(_isReferAFriend)
                {
                    isSimpleActivation = false;
                    isGiftCard = false;
                }
                
                RaisePropertyChanged(() => isReferAFriend);
            }
        }

        public bool isPartial
        {
            get
            {
                return _isPartial;
            }
            set
            {
                _isPartial = value;
                RaisePropertyChanged(() => isPartial);
            }
        }

        public bool isRecreateBill
        {
            get
            {
                return _isRecreateBill;
            }
            set
            {
                _isRecreateBill = value;
                RaisePropertyChanged(() => isRecreateBill);
            }
        }

        /*public Imaa QRImage
         {
             get
             {
                 return _QRImage;
             }
             set
             {
                 _QRImage = value;
                 RaisePropertyChanged(() => QRImage);
             }
         }*/

        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged(() => title);
            }
        }

        
        public string textMessage
        {
            get
            {
                return _textMessage;
            }
            set
            {
                _textMessage = value;
                RaisePropertyChanged(() => textMessage);
            }
        }
        
        public string phoneMobile
        {
            get
            {
                return _phoneMobile;
            }
            set
            {
                _phoneMobile = value;
                RaisePropertyChanged(() => phoneMobile);
            }
        }
        
        public string processingAddress
        {
            get
            {
                return _processingAddress;
            }
            set
            {
                _processingAddress = value;
                RaisePropertyChanged(() => processingAddress);
            }
        }
        
        public string cardNumForActivation
        {
            get
            {
                return _cardNumForActivation;
            }
            set
            {
                _cardNumForActivation = value;
                RaisePropertyChanged(() => cardNumForActivation);
            }
        }

        public string terminalIdForActivationBMS
        {
            get
            {
                return _terminalIdForActivationBMS;
            }
            set
            {
                _terminalIdForActivationBMS = value;
                RaisePropertyChanged(() => terminalIdForActivationBMS);
            }
        }

        public string accountType
        {
            get
            {
                return _accountType;
            }
            set
            {
                _accountType = value;
                RaisePropertyChanged(() => accountType);
            }
        }

        public string cardNumForLockUnlock
        {
            get
            {
                return _cardNumForLockUnlock;
            }
            set
            {
                _cardNumForLockUnlock = value;
                RaisePropertyChanged(() => cardNumForLockUnlock);
            }
        }

        public string reportData
        {
            get
            {
                return _reportData;
            }
            set
            {
                _reportData = value;
                RaisePropertyChanged(() => reportData);
            }
        }
        
        public string beginDate
        {
            get
            {
                return _beginDate;
            }
            set
            {
                _beginDate = value;
                RaisePropertyChanged(() => beginDate);
            }
        }

        public string endDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                RaisePropertyChanged(() => endDate);
            }
        }

        public string hashCardNum
        {
            get
            {
                return _hashCardNum;
            }
            set
            {
                _hashCardNum = value;
                RaisePropertyChanged(() => hashCardNum);
            }
        }

        public string protocolVersion
        {
            get
            {
                if (_protocolVersion == null)
                {
                    _protocolVersion = "0.1";
                }
                return _protocolVersion;
            }
            set
            {
                _protocolVersion = value;
                RaisePropertyChanged(() => protocolVersion);
                Config.protocolVersion = protocolVersion;
            }
        }

        public string terminalId
        {
            get
            {
                return _terminalId;
            }
            set
            {
                _terminalId = value;
                RaisePropertyChanged(() => terminalId);
                Config.terminalId = terminalId;
            }
        }

        public string retailNetworkName
        {
            get
            {
                return _retailNetworkName;
            }
            set
            {
                _retailNetworkName = value;
                RaisePropertyChanged(() => retailNetworkName);
                Config.retailNetworkName = retailNetworkName;
            }
        }

        public string retailPointAddress
        {
            get
            {
                return _retailPointAddress;
            }
            set
            {
                _retailPointAddress = value;
                RaisePropertyChanged(() => retailPointAddress);
                Config.retailPointAddress = retailPointAddress;
            }
        } 

        public string serverAddr
        {
            get
            {
                return _serverAddr;
            }
            set
            {
                _serverAddr = value;
                RaisePropertyChanged(() => serverAddr);
                Config.serverAddr = serverAddr;
            }
        }

        public string serverAltAddr
        {
            get
            {
                return _serverAltAddr;
            }
            set
            {
                _serverAltAddr = value;
                RaisePropertyChanged(() => serverAltAddr);
                Config.serverAltAddr = serverAltAddr;
            }
        }

        public string sslKey
        {
            get
            {
                if (_sslKey == null)
                {
                    _sslKey = "privkey.pem";
                }
                return _sslKey;
            }
            set
            {
                _sslKey = value;
                RaisePropertyChanged(() => sslKey);
                Config.sslKey = sslKey;
            }
        }

        public string billXMLConvertTable
        {
            get
            {
                if (_billXMLConvertTable == null)
                {
                    _billXMLConvertTable = "UTF8";
                }
                return _billXMLConvertTable;
            }
            set
            {
                _billXMLConvertTable = value;
                RaisePropertyChanged(() => billXMLConvertTable);
                Config.billXMLConvertTable = billXMLConvertTable;
            }
        }

        public string terminalPassword
        {
            get
            {
                return _terminalPassword;
            }
            set
            {
                _terminalPassword = value;
                RaisePropertyChanged(() => terminalPassword);
                Config.terminalPassword = terminalPassword;
            }
        }

        public string billFile
        {
            get
            {
                return _billFile;
            }
            set
            {
                _billFile = value;
                RaisePropertyChanged(() => billFile);
            }
        }

        public string referFriendCard
        {
            get
            {
                return _referFriendCard;
            }
            set
            {
                _referFriendCard = value;
                RaisePropertyChanged(() => referFriendCard);
            }
        }

        public string linkcardNum
        {
            get
            {
                return _linkcardNum;
            }
            set
            {
                _linkcardNum = value;
                RaisePropertyChanged(() => linkcardNum);
            }
        }

        public string cashBackFile
        {
            get
            {
                return _cashBackFile;
            }
            set
            {
                _cashBackFile = value;
                RaisePropertyChanged(() => cashBackFile);
            }
        }

        public string cashBackRrn
        {
            get
            {
                return _cashBackRrn;
            }
            set
            {
                _cashBackRrn = value;
                RaisePropertyChanged(() => cashBackRrn);
            }
        }

        public string rrn
        {
            get
            {
                return _rrn;
            }
            set
            {
                _rrn = value;
                RaisePropertyChanged(() => rrn);
            }
        }

        public string token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
                RaisePropertyChanged(() => token);
            }
        }

        public string cardNum
        {
            get
            {
                return _cardNum;
            }
            set
            {
                _cardNum = value;
                RaisePropertyChanged(() => cardNum);
            }
        }

        public string cardNumForHashEP
        {
            get
            {
                return _cardNumForHashEP;
            }
            set
            {
                _cardNumForHashEP = value;
                RaisePropertyChanged(() => cardNumForHashEP);
            }
        }

        public string hashCardEP
        {
            get
            {
                return _hashCardEP;
            }
            set
            {
                _hashCardEP = value;
                RaisePropertyChanged(() => hashCardEP);
            }
        }
        
        public string receiptData
        {
            get
            {
                return _receiptData;
            }
            set
            {
                _receiptData = value;
                RaisePropertyChanged(() => receiptData);
            }
        }

        public string receiptHeader
        {
            get
            {
                return _receiptHeader;
            }
            set
            {
                _receiptHeader = value;
                RaisePropertyChanged(() => receiptHeader);
            }
        }

        public string receiptMainHeader
        {
            get
            {
                return _receiptMainHeader;
            }
            set
            {
                _receiptMainHeader = value;
                RaisePropertyChanged(() => receiptMainHeader);
            }
        }

        public string receiptOrganizerFee
        {
            get
            {
                return _receiptOrganizerFee;
            }
            set
            {
                _receiptOrganizerFee = value;
                RaisePropertyChanged(() => receiptOrganizerFee);
            }
        }

        public string receiptFooter
        {
            get
            {
                return _receiptFooter;
            }
            set
            {
                _receiptFooter = value;
                RaisePropertyChanged(() => receiptFooter);
            }
        }

        public double cashBackSum
        {
            get
            {
                return _cashBackSum;
            }
            set
            {
                _cashBackSum = value;
                RaisePropertyChanged(() => cashBackSum);
            }
        }


        public double depositSum
        {
            get
            {
                return _depositSum;
            }
            set
            {
                _depositSum = value;
                RaisePropertyChanged(() => depositSum);
            }
        }

        public double giftSum
        {
            get
            {
                return _giftSum;
            }
            set
            {
                _giftSum = value;
                RaisePropertyChanged(() => giftSum);
            }
        }

        public double activationAmount
        {
            get
            {
                return _activationAmount;
            }
            set
            {
                _activationAmount = value;
                RaisePropertyChanged(() => activationAmount);
            }
        }

        public int selectItem
        {
            get
            {
                if (!String.IsNullOrEmpty(Profiles[_selectItem].dataSource))
                    dataSource = Crypto.SimpleDecryptWithPassword(Profiles[_selectItem].dataSource, "2346dfgxdr6fjufcgbjdcgfh");

                terminalId = Profiles[_selectItem].terminalId;
                serverAddr = Profiles[_selectItem].serverAddr;
                processingAddress = Profiles[_selectItem].serverAddr.Remove(serverAddr.Length - 11, 11);
                terminalPassword = Profiles[_selectItem].terminalPassword;
                sslMode = Profiles[_selectItem].sslMode;
                cardNum = Profiles[_selectItem].cardNum;

                terminalIdForActivationBMS = Profiles[_selectItem].terminalId;
                cardNumForActivation = Profiles[_selectItem].cardNum;

                AdditionalFunc.SaveConfig(Config);

                if(File.Exists("STATE"))
                    File.Delete("STATE");

                return _selectItem;
            }
            set
            {
                _selectItem = value;
                RaisePropertyChanged(() => selectItem);
            }
        }

        public int sslMode
        {
            get
            {
                return _sslMode;
            }
            set
            {
                _sslMode = value;
                RaisePropertyChanged(() => sslMode);
                Config.sslMode = sslMode;
            }
        }

        public int shiftMode
        {
            get
            {
                return _shiftMode;
            }
            set
            {
                _shiftMode = value;
                RaisePropertyChanged(() => shiftMode);
                Config.shiftMode = shiftMode;
            }
        }

        public int hashCardNumConfig
        {
            get
            {
                return _hashCardNumConfig;
            }
            set
            {
                _hashCardNumConfig = value;
                RaisePropertyChanged(() => hashCardNumConfig);
                Config.hashCardNum = hashCardNumConfig;
            }
        }

        public int logSize
        {
            get
            {
                return _logSize;
            }
            set
            {
                _logSize = value;
                RaisePropertyChanged(() => logSize);
                Config.logSize = logSize;
            }
        }

        public int orgFeeOffline
        {
            get
            {
                return _orgFeeOffline;
            }
            set
            {
                _orgFeeOffline = value;
                RaisePropertyChanged(() => orgFeeOffline);
                Config.orgFeeOffline = orgFeeOffline;
            }
        }

        public int offlineMode
        {
            get
            {
                return _offlineMode;
            }
            set
            {
                _offlineMode = value;
                RaisePropertyChanged(() => offlineMode);
                Config.offlineMode = offlineMode;
            }
        }

        public int timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
                RaisePropertyChanged(() => timeout);
                Config.timeout = timeout;
            }
        }

        public int connect_timeout
        {
            get
            {
                return _connect_timeout;
            }
            set
            {
                _connect_timeout = value;
                RaisePropertyChanged(() => connect_timeout);
                Config.connect_timeout = connect_timeout;
            }
        }

        public int retry_pause
        {
            get
            {
                return _retry_pause;
            }
            set
            {
                _retry_pause = value;
                RaisePropertyChanged(() => retry_pause);
                Config.retry_pause = retry_pause;
            }
        }

        public int noLocalBonusPrint
        {
            get
            {
                return _noLocalBonusPrint;
            }
            set
            {
                _noLocalBonusPrint = value;
                RaisePropertyChanged(() => noLocalBonusPrint);
            }
        }

        public int noDiscountBonusPrint
        {
            get
            {
                return _noDiscountBonusPrint;
            }
            set
            {
                _noDiscountBonusPrint = value;
                RaisePropertyChanged(() => noDiscountBonusPrint);
            }
        }

        public int noGlobalBonusPrint
        {
            get
            {
                return _noGlobalBonusPrint;
            }
            set
            {
                _noGlobalBonusPrint = value;
                RaisePropertyChanged(() => noGlobalBonusPrint);
            }
        }

        public int globalBonusOffline_1
        {
            get
            {
                return _globalBonusOffline_1;
            }
            set
            {
                _globalBonusOffline_1 = value;
                RaisePropertyChanged(() => globalBonusOffline_1);
                Config.class_card = new class_card();
                Config.class_card._1 = new _1();
                Config.class_card._1.globalBonusOffline = globalBonusOffline_1;
            }
        }

        public int globalBonusOffline_2
        {
            get
            {
                return _globalBonusOffline_2;
            }
            set
            {
                _globalBonusOffline_2 = value;
                RaisePropertyChanged(() => globalBonusOffline_2);
                Config.class_card._2 = new _2();
                Config.class_card._2.globalBonusOffline = globalBonusOffline_2;
            }
        }

        public int globalBonusOffline_5
        {
            get
            {
                return _globalBonusOffline_5;
            }
            set
            {
                _globalBonusOffline_5 = value;
                RaisePropertyChanged(() => globalBonusOffline_5);
                Config.class_card._5 = new _5();
                Config.class_card._5.globalBonusOffline = globalBonusOffline_5;
            }
        }

        public int globalBonusOffline_6
        {
            get
            {
                return _globalBonusOffline_6;
            }
            set
            {
                _globalBonusOffline_6 = value;
                RaisePropertyChanged(() => globalBonusOffline_6);
                Config.class_card._6 = new _6();
                Config.class_card._6.globalBonusOffline = globalBonusOffline_6;
            }
        }

        public int localBonusOffline_1
        {
            get
            {
                return _localBonusOffline_1;
            }
            set
            {
                _localBonusOffline_1 = value;
                RaisePropertyChanged(() => localBonusOffline_1);
                Config.class_card._1.localBonusOffline = localBonusOffline_1;
            }
        }

        public int localBonusOffline_2
        {
            get
            {
                return _localBonusOffline_2;
            }
            set
            {
                _localBonusOffline_2 = value;
                RaisePropertyChanged(() => localBonusOffline_2);
                Config.class_card._2.localBonusOffline = localBonusOffline_2;
            }
        }

        public int localBonusOffline_5
        {
            get
            {
                return _localBonusOffline_5;
            }
            set
            {
                _localBonusOffline_5 = value;
                RaisePropertyChanged(() => localBonusOffline_5);
                Config.class_card._5.localBonusOffline = localBonusOffline_5;
            }
        }

        public int localBonusOffline_6
        {
            get
            {
                return _localBonusOffline_6;
            }
            set
            {
                _localBonusOffline_6 = value;
                RaisePropertyChanged(() => localBonusOffline_6);
                Config.class_card._6.localBonusOffline = localBonusOffline_6;
            }
        }

        public int sentSpendOnActivation
        {
            get
            {
                return _sentSpendOnActivation;
            }
            set
            {
                _sentSpendOnActivation = value;
                RaisePropertyChanged(() => sentSpendOnActivation);
            }
        }

        public int timeZone
        {
            get
            {
                if (_timeZone == 0)
                {
                    _timeZone = 3;
                }
                return _timeZone;
            }
            set
            {
                _timeZone = value;
                RaisePropertyChanged(() => timeZone);
                Config.timeZone = timeZone;
            }
        }

        public bool isEnabledUpdate
        {
            get
            {
                return _isEnabledUpdate;
            }
            set
            {
                _isEnabledUpdate = value;
                RaisePropertyChanged(() => isEnabledUpdate);
            }
        }

        public ICommand SendSMSOperation
        {
            get
            {
                return _SendSMSOperation ?? (_SendSMSOperation = new RelayCommand(() =>
                {
                    BMSRequests.SMSSend(processingAddress, phoneMobile, textMessage);
                }));
            }
        }

        public ICommand CloseShiftOperation
        {
            get
            {
                return _CloseShiftOperation ?? (_CloseShiftOperation = new RelayCommand(() =>
                {
                    Requests.CloseShift();
                }));
            }
        }

        public ICommand OpenShiftOperation
        {
            get
            {
                return _OpenShiftOperation ?? (_OpenShiftOperation = new RelayCommand(() =>
                {
                    Requests.OpenShift(null);
                }));
            }
        }

        public ICommand SendPushOperation
        {
            get
            {
                return _SendPushOperation ?? (_SendPushOperation = new RelayCommand(() =>
                {
                    BMSRequests.PushSend(dataSource, phoneMobile, textMessage);
                }));
            }
        }
        
        public ICommand Update
        {
            get
            {
                return _Update ?? (_Update = new RelayCommand(() =>
                {
                    if (UpdateMethods.Update())
                        isEnabledUpdate = false;
                }));
            }
        }

        public ICommand RefreshActionsOperation
        {
            get
            {
                return _RefreshActionsOperation ?? (_RefreshActionsOperation = new RelayCommand(() =>
                {
                    BMSRequests.RefreshActionsOperation(processingAddress);
                }));
            }
        }
        
        public ICommand BMSUnLockCardOperation
        {
            get
            {
                return _BMSUnLockCardOperation ?? (_BMSUnLockCardOperation = new RelayCommand(() =>
                {
                    BMSRequests.BMSUnLockCardOperation(dataSource, serverAddr, cardNum);
                }));
            }
        }
        
        public ICommand BMSLockCardOperation
        {
            get
            {
                return _BMSLockCardOperation ?? (_BMSLockCardOperation = new RelayCommand(() =>
                {
                    BMSRequests.BMSLockCardOperation(dataSource, serverAddr, cardNum, isTokenNeed);
                }));
            }
        }

        public ICommand BMSSendTokenOperation
        {
            get
            {
                return _BMSSendTokenOperation ?? (_BMSSendTokenOperation = new RelayCommand(() =>
                {
                    BMSRequests.BMSSendTokenOperation(dataSource, serverAddr, cardNum);
                }));
            }
        }

        public ICommand BMSActivationOperation
        {
            get
            {
                return _BMSActivationOperation ?? (_BMSActivationOperation = new RelayCommand(() =>
                {
                    BMSRequests.BMSActivationOperation(serverAddr, terminalIdForActivationBMS, activationAmount, accountType, cardNumForActivation);

                    cardNum = cardNumForActivation;
                }));
            }
        }
        
        public ICommand BMSConfirmOperation
        {
            get
            {
                return _BMSConfirmOperation ?? (_BMSConfirmOperation = new RelayCommand(() =>
                {
                    if(!String.IsNullOrEmpty(rrn))
                        BMSRequests.BMSConfirmOperation(dataSource, serverAddr, rrn);
                }));
            }
        }

        public ICommand BMSCancelOperation
        {
            get
            {
                return _BMSCancelOperation ?? (_BMSCancelOperation = new RelayCommand(() =>
                {
                    if (!String.IsNullOrEmpty(rrn))
                        BMSRequests.BMSCancelOperation(dataSource, serverAddr, rrn);
                }));
            }
        }

        public ICommand HashCardFindTime
        {
            get
            {
                return _hashCardFindTime ?? (_hashCardFindTime = new RelayCommand(() =>
                {
                    if(!String.IsNullOrEmpty(hashCardEP) & hashCardEP.Length > 39)
                        AdditionalFunc.HashCardFindTime(dataSource, hashCardEP);
                }));
            }
        }

        public ICommand HashCardEPOperation
        {
            get
            {
                return _hashCardEPOperation ?? (_hashCardEPOperation = new RelayCommand(() =>
                {
                    hashCardEP = AdditionalFunc.HashCardExtraPayment(dataSource, cardNumForHashEP, isEnableExtraPayment);
                    cardNum = hashCardEP;
                    //Zen.Barcode.CodeQrBarcodeDraw qrCode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                    /*
                    using (var generator = new BarcodeGenerator(EncodeTypes.QR))
                    {
                        generator.CodeText = hashCardEP;
                        generator.Save("code128.png");
                    }
                    */
                    //QRImage = "code128.png";
                }));
            }
        }

        
        public ICommand SummaryReportOperation
        {
            get
            {
                return _summaryReportOperation ?? (_summaryReportOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) != 1)
                        //reportData = Requests.SummaryReportOperation(String.Format("{0:yyyy-MM-ddTHH:mm:ss}", beginDate), String.Format("{0:yyyy-MM-ddTHH:mm:ss}", endDate));
                        reportData = Requests.SummaryReportOperation(null, null);

                    Requests.AfterOperation(); 
                }));
            }
        }

        public ICommand DetailReportOperation
        {
            get
            {
                return _detailReportOperation ?? (_detailReportOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) != 1)
                        //reportData = Requests.DetailReportOperation(String.Format("{0:yyyy-MM-ddTHH24:mm:ss}", beginDate), String.Format("{0:yyyy-MM-ddTHH24:mm:ss}", endDate));
                        reportData = Requests.DetailReportOperation(null, null);

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand EarnOperation
        {
            get
            {
                return _earnOperation ?? (_earnOperation = new RelayCommand(() =>
                {
                    if (isRecreateBill)
                        Bill.BillMakeFile(billFile, Bill.BillParse(billFile));

                    if (Requests.BeforeOperation(cardNum) != 1)
                    {
                        if (isWithoutConfirm)
                        {
                            if (Requests.PaymentOperation(0, billFile) != 1)
                            {
                                receiptData = Requests.BonusReceipt();
                                rrn = Requests.GetRRN();
                                cashBackRrn = rrn;
                            }
                        }
                        else
                        {
                            if (Requests.PaymentAndConfirmOperation(0, billFile) != 1)
                            {
                                receiptData = Requests.BonusReceipt();
                                rrn = Requests.GetRRN();
                                cashBackRrn = rrn;
                            }
                        }             
                    }

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand SpendOperation
        {
            get
            {
                return _spendOperation ?? (_spendOperation = new RelayCommand(() =>
                {
                    if (isRecreateBill)
                        Bill.BillMakeFile(billFile, Bill.BillParse(billFile));

                    if (Requests.BeforeOperation(cardNum) != 1)
                    {
                        if (isWithoutConfirm)
                        {
                            if (Requests.PaymentOperation(1, billFile) != 1)
                            {
                                receiptData = Requests.BonusReceipt();
                                rrn = Requests.GetRRN();
                                cashBackRrn = rrn;
                            }
                        }
                        else
                        {
                            if (Requests.PaymentAndConfirmOperation(1, billFile) != 1)
                            {
                                receiptData = Requests.BonusReceipt();
                                rrn = Requests.GetRRN();
                                cashBackRrn = rrn;
                            }
                        }
                    }

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand SendOfflineOperation
        {
            get
            {
                return _sendOfflineOperation ?? (_sendOfflineOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) != 1)
                        Requests.SendOfflineOperation();

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand CardNumFromHashOperation
        {
            get
            {
                return _cardNumFromHashOperation ?? (_cardNumFromHashOperation = new RelayCommand(() =>
                {
                    if(hashCardEP.Length>39)
                        cardNumForHashEP = AdditionalFunc.CardNumFromHash(dataSource, hashCardEP);
                }));
            }
        }
        
        public ICommand HashCardNumOperation
        {
            get
            {
                return _hashCardNumOperation ?? (_hashCardNumOperation = new RelayCommand(() =>
                {
                    hashCardNum = Requests.HashCardNumOperation(cardNum);
                }));
            }
        }

        public ICommand SaveConfig
        {
            get
            {
                return _saveConfig ?? (_saveConfig = new RelayCommand(() =>
                {
                    AdditionalFunc.SaveConfig(Config);
                }));
            }
        }

        public ICommand ConfirmOperation
        {
            get
            {
                return _confirmOperation ?? (_confirmOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) != 1)
                        if (Requests.ConfirmOperation(rrn) != 1)
                        {
                            receiptData = Requests.BonusReceipt();
                            rrn = Requests.GetRRN();
                            cashBackRrn = rrn;
                        }

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand CashBackOperation
        {
            get
            {
                return _cashBackOperation ?? (_cashBackOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) != 1)
                    {
                        if (!isPartial)
                            cashBackFileTemp = null;
                        else
                            cashBackFileTemp = cashBackFile;

                        if (isSecondTypeCashBack)
                        {
                            if (Requests.CashBackSecondTypeOperation(cashBackRrn, Convert.ToInt32(cashBackSum * 100)) != 1)
                            {
                                receiptData = Requests.BonusReceipt();
                                rrn = Requests.GetRRN();
                                cashBackRrn = rrn;
                            }
                        }
                        else
                        {
                            if (Requests.CashBackFirstTypeOperation(cashBackRrn, cashBackFileTemp) != 1)
                            {
                                receiptData = Requests.BonusReceipt();
                                rrn = Requests.GetRRN();
                                cashBackRrn = rrn;
                            }
                        }
                    }

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand ActivationOperation
        {
            get
            {
                return _activationOperation ?? (_activationOperation = new RelayCommand(() =>
                {
                    if (isRecreateBill)
                        Bill.BillMakeFile(billFile, Bill.BillParse(billFile));

                    if (Requests.BeforeOperation(cardNum) != 1)
                    {
                        string activation_type = String.Empty;

                        if (isSimpleActivation) activation_type = "SIMPLE_ACTIVATION";
                        if (isGiftCard) activation_type = "GIFT_CARD";
                        if (isReferAFriend) activation_type = "REFER_A_FRIEND";

                        if (Requests.ActivationOperation(activation_type, billFile, Convert.ToInt32(giftSum * 100), "GLOBAL", referFriendCard) != 1)
                        {
                            receiptData = Requests.BonusReceipt();
                            rrn = Requests.GetRRN();
                            cashBackRrn = rrn;
                        }
                    }

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand LinkCardOperation
        {
            get
            {
                return _linkCardOperation ?? (_linkCardOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) != 1)
                        if (Requests.LinkCardOperation(linkcardNum) != 1)
                        {
                            receiptData = Requests.BonusReceipt();
                            rrn = Requests.GetRRN();
                            cashBackRrn = rrn;
                        }

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand ExchangeOperation
        {
            get
            {
                return _exchangeOperation ?? (_exchangeOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) != 1)
                        if (Requests.ExchangeOperation(token) != 1)
                        {
                            receiptData = Requests.BonusReceipt();
                            rrn = Requests.GetRRN();
                            cashBackRrn = rrn;
                        }

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand CancelOperation
        {
            get
            {
                return _cancelOperation ?? (_cancelOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) != 1)
                        if (Requests.CancelOperation(rrn) != 1)
                        {
                            receiptData = Requests.BonusReceipt();
                            rrn = Requests.GetRRN();
                            cashBackRrn = rrn;
                        }

                    Requests.AfterOperation();
                }));
            }
        }
        
        public ICommand PaymentCancelOperation
        {
            get
            {
                return _paymentCancelOperation ?? (_paymentCancelOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) != 1)
                        if (Requests.PaymentCancelOperation(rrn) != 1)
                        {
                            receiptData = Requests.BonusReceipt();
                            rrn = Requests.GetRRN();
                            cashBackRrn = rrn;
                        }

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand BalanceOperation
        {
            get
            {
                return _balanceOperation ?? (_balanceOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) !=1)
                        if (Requests.BalanceOperation() != 1)
                        {
                            receiptData = Requests.BonusReceipt();
                            rrn = Requests.GetRRN();
                            cashBackRrn = rrn;
                        }

                    Requests.AfterOperation();
                }));
            }
        }

        public ICommand DepositOperation
        {
            get
            {
                return _depositOperation ?? (_depositOperation = new RelayCommand(() =>
                {
                    if (Requests.BeforeOperation(cardNum) != 1)
                        if (Requests.DepositOperation(Convert.ToInt32(depositSum*100)) != 1)
                        {
                            receiptData = Requests.BonusReceipt();
                            rrn = Requests.GetRRN();
                            cashBackRrn = rrn;
                        }

                    Requests.AfterOperation();
                }));
            }
        }

        public ViewModel()
        {
            if (UpdateMethods.Update())
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }

            Config Config = new Config();
            ConfigProfiles = new ObservableCollection<string> { };

            Config = AdditionalFunc.ReadConfig();
            Profiles = ProfilesMethods.ReadProfiles();

            if (Profiles[0].dataSource.Contains("Data Source"))
                ProfilesMethods.SaveProfiles(Profiles);

            terminalId = Config.terminalId;
            serverAddr = Config.serverAddr;
            serverAltAddr = Config.serverAltAddr;
            terminalPassword = Config.terminalPassword;
            sslMode = Config.sslMode;
            sslKey = Config.sslKey;
            hashCardNumConfig = Config.hashCardNum;
            shiftMode = Config.shiftMode;
            protocolVersion = Config.protocolVersion;
            //sentSpendOnActivation = Config.sentSpendOnActivation;
            logSize = Config.logSize;
            timeZone = Config.timeZone;
            retailNetworkName = Config.retailNetworkName;
            retailPointAddress= Config.retailPointAddress;
            //receiptHeader = Config.receiptHeader;
            //receiptMainHeader = Config.receiptMainHeader;
            //receiptOrganizerFee = Config.receiptOrganizerFee;
            //receiptFooter = Config.receiptFooter;
            //noLocalBonusPrint = Config.noLocalBonusPrint;
            //noDiscountBonusPrint = Config.noDiscountBonusPrint;
            //noGlobalBonusPrint = Config.noGlobalBonusPrint;
            timeout = Config.timeout;
            connect_timeout = Config.connect_timeout;
            retry_pause = Config.retry_pause;
            offlineMode = Config.offlineMode;
            orgFeeOffline = Config.orgFeeOffline;
            billXMLConvertTable = Config.billXMLConvertTable;

            globalBonusOffline_1 = Config.class_card._1.globalBonusOffline;
            globalBonusOffline_2 = Config.class_card._2.globalBonusOffline;
            globalBonusOffline_5 = Config.class_card._5.globalBonusOffline;
            globalBonusOffline_6 = Config.class_card._6.globalBonusOffline;
            localBonusOffline_1 = Config.class_card._1.localBonusOffline;
            localBonusOffline_2 = Config.class_card._2.localBonusOffline;
            localBonusOffline_5 = Config.class_card._5.localBonusOffline;
            localBonusOffline_6 = Config.class_card._6.localBonusOffline;

            for (int i=0; i<Profiles.Length; i++)
                ConfigProfiles.Add(Profiles[i].profileName);
        }
    }
}
