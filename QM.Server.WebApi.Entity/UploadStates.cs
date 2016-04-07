namespace QM.Server.WebApi.Entity {
    public enum UploadStates {
        Success = 0,
        NotMutiPart,
        NoFileFoundFromRequest,
        NotZipFile,
        InvalidName,
        NameExists
    }
}
