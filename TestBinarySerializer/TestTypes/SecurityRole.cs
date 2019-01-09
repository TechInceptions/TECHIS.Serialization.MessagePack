

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace TECHIS.EndApps.AppModel.Business
{

    /// <summary>
    /// This is the Primary Domain Model class for SecurityRole.
    /// </summary>
    public partial class SecurityRole
    {

        #region Fields 

        private Guid _ApplicationId;
        private Guid _RoleId;
        private String _RoleName;
        private String _LoweredRoleName;
        private String _Description;
        private Int32 _ApplicationRegisterId;
        //private List<TECHIS.EndApps.AppModel.Business.RoleMember> _RoleMemberList;

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

        public virtual Guid RoleId
        {

            get
            {
                return _RoleId;
            }

            set
            {
                _RoleId = value;
            }
        }

        public virtual String RoleName
        {

            get
            {
                return _RoleName;
            }

            set
            {
                _RoleName = value;
            }
        }

        public virtual String LoweredRoleName
        {

            get
            {
                return _LoweredRoleName;
            }

            set
            {
                _LoweredRoleName = value;
            }
        }

        /// <summary>
        /// This is not a required field.
        /// </summary>
        public virtual String Description
        {

            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
            }
        }

        public virtual Int32 ApplicationRegisterId
        {

            get
            {
                return _ApplicationRegisterId;
            }

            set
            {
                _ApplicationRegisterId = value;
            }
        }





        #endregion 

        #region Default Constructor 

        public SecurityRole()
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

        public SecurityRole(Guid roleId)
        {

            RoleId = roleId;

            Load();
        }



        #endregion 

        #region Public Methods 


        /// <summary>
        /// Get the instance of SecurityRole that corresponds to the primary key. If no data is found, a null instance is returned.
        /// Returns an instance of SecurityRole.
        /// </summary>
        public static SecurityRole GetSecurityRole(Guid roleId)
        {
            return new SecurityRole();
        }
        

        #endregion

    }

}






