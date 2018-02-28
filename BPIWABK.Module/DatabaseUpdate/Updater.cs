using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using BPIWABK.Module.BusinessObjects.Administrative;

namespace BPIWABK.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            Pegawai sampleUser = ObjectSpace.FindObject<Pegawai>(new BinaryOperator("UserName", "User"));
            if (sampleUser == null)
            {
                sampleUser = ObjectSpace.CreateObject<Pegawai>();
                sampleUser.UserName = "User";
                sampleUser.SetPassword("");
                sampleUser.NamaLengkap = "Standard User";
            }
            PermissionPolicyRole defaultRole = CreateDefaultRole();
            sampleUser.Roles.Add(defaultRole);

            Pegawai userAdmin = ObjectSpace.FindObject<Pegawai>(new BinaryOperator("UserName", "Admin"));
            if (userAdmin == null)
            {
                userAdmin = ObjectSpace.CreateObject<Pegawai>();
                userAdmin.UserName = "Admin";
                userAdmin.NamaLengkap = "Super Administrator";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");
            }
            // If a role with the Administrators name doesn't exist in the database, create this role
            Peran adminRole = ObjectSpace.FindObject<Peran>(new BinaryOperator("Name", "Administrators"));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<Peran>();
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;
            userAdmin.Roles.Add(adminRole);
            ObjectSpace.CommitChanges(); //This line persists created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            if (ObjectSpace.GetObjectsCount(typeof(Setelan), null) == 0)
            {
                ObjectSpace.CreateObject<Setelan>();
            }

            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
        private Peran CreateDefaultRole()
        {
            Peran defaultRole = ObjectSpace.FindObject<Peran>(new BinaryOperator("Name", "Default"));
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<Peran>();
                defaultRole.Name = "Default";

                defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<Peran>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
            }
            return defaultRole;
        }
    }
}
