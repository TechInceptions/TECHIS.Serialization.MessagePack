

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace TECHIS.EndApps.AppModel.Business
{

    /// <summary>
    /// This is the Primary Domain Model class for RoleMember.
    /// </summary>
    public partial class RoleMember
    {

        #region Fields 

        private Guid _ApplicationId;
        private Guid _UserId;
        private String _UserName;
        private String _LoweredUserName;
        private String _MobileAlias;
        private Boolean _IsAnonymous;
        private DateTime _LastActivityDate;
        private List<SecurityRole> _SecurityRoleList = new List<SecurityRole>();

        #endregion 

        #region Properties 

        public virtual Guid ApplicationId
        {

            get
            {
                return _ApplicationId;
            }

            set
            {
                _ApplicationId = value;
            }
        }

        public virtual Guid UserId
        {

            get
            {
                return _UserId;
            }

            set
            {
                _UserId = value;
            }
        }

        public virtual String UserName
        {

            get
            {
                return _UserName;
            }

            set
            {
                _UserName = value;
            }
        }

        public virtual String LoweredUserName
        {

            get
            {
                return _LoweredUserName;
            }

            set
            {
                _LoweredUserName = value;
            }
        }

        /// <summary>
        /// This is not a required field.
        /// </summary>
        public virtual String MobileAlias
        {

            get
            {
                return _MobileAlias;
            }

            set
            {
                _MobileAlias = value;
            }
        }

        public virtual Boolean IsAnonymous
        {

            get
            {
                return _IsAnonymous;
            }

            set
            {
                _IsAnonymous = value;
            }
        }

        public virtual DateTime LastActivityDate
        {

            get
            {
                return _LastActivityDate;
            }

            set
            {
                _LastActivityDate = value;
            }
        }

        public List<SecurityRole> SecurityRoleList
        {
            get
            {
                return _SecurityRoleList;
            }
            set
            {
                _SecurityRoleList = value;
            }

        }

        #endregion

        #region Default Constructor 

        public RoleMember()
        {
        }

        #endregion 

        #region Private Methods 


        /// <summary>
        ///Populates the instance using data from the datastore.  
        ///The lookup of the data in the datastore is based on the instance's current ID value.
        /// </summary>
        private void Load()
        {



        }

        #endregion 

        #region Constructors 

        #endregion 


    }

}





