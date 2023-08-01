// Copyright 2002-2004 ArdeshirV, Licensed Under MIT
// https://github.com/ArdeshirV/2D-Vector
unit _2DVectorInfoMain;

{$WARN SYMBOL_PLATFORM OFF}

interface

uses
  Windows, ActiveX, Classes, ComObj, ShlObj;

type
  TInfoTipHandler = class(TComObject, IQueryInfo, IPersistFile)
  private
    FFileName: string;
    FMalloc: IMalloc;
  protected
    { IQUeryInfo }
    function GetInfoTip(dwFlags: DWORD; var ppwszTip: PWideChar): HResult;
      stdcall;
    function GetInfoFlags(out pdwFlags: DWORD): HResult; stdcall;
    {IPersist}
    function GetClassID(out classID: TCLSID): HResult; stdcall;
    { IPersistFile }
    function IsDirty: HResult; stdcall;
    function Load(pszFileName: POleStr; dwMode: Longint): HResult; stdcall;
    function Save(pszFileName: POleStr; fRemember: BOOL): HResult; stdcall;
    function SaveCompleted(pszFileName: POleStr): HResult; stdcall;
    function GetCurFile(out pszFileName: POleStr): HResult; stdcall;
  public
    procedure Initialize; override;
  end;

  TInfoTipFactory = class(TComObjectFactory)
  protected
    function GetProgID: string; override;
    procedure ApproveShellExtension(Register: Boolean; const ClsID: string);
      virtual;
  public
    procedure UpdateRegistry(Register: Boolean); override;
  end;

const
  Class_InfoTipHandler: TGUID = '{32425318-C870-44E1-B73A-8172C8E76A5B}';


implementation

uses ComServ, SysUtils, Registry, UXmlNode;

function GetInformation(const FileName: string): PWideChar;

  function VectorToStr(const vector: IXMLVectorType): WideString;
  begin
    Result := '[' +
      CurrToStr(vector.EndPoint.X - vector.StartPoint.X) + ',' +
      CurrToStr(vector.EndPoint.Y - vector.StartPoint.Y) + '], ';
  end;

  function GetResultVector(const xml: IXMLVectorsType): string;
  var
    l_dblX,
    l_dblY: double;
    l_intIndexer: integer;
  begin
    l_dblX := 0.0;
    l_dblY := 0.0;

    for l_intIndexer := 0 to xml.Count - 1 do
    begin
      l_dblX := l_dblX + xml.Vector[l_intIndexer].EndPoint.X -
                         xml.Vector[l_intIndexer].StartPoint.X;
      l_dblY := l_dblY + xml.Vector[l_intIndexer].EndPoint.Y -
                         xml.Vector[l_intIndexer].StartPoint.Y;
    end;

    Result := '[' + CurrToStr(l_dblX) + ',' + CurrToStr(l_dblY) +'], Length = '
      + CurrToStr(sqrt(l_dblX * l_dblX + l_dblY * l_dblY));
  end;

var
  l_str: WideString;
  l_blnGreat: Boolean;
  l_intCount: integer;
  l_intIndexer: integer;
  l_xml: IXMLVectorsType;
const
  NewLine = #10#13;
begin
  l_xml := LoadVectors(FileName);
  l_str := 'Type: 2D Vector Document';
  l_blnGreat := l_xml.Count - 1 > 36;

  if l_blnGreat then
    l_intCount := 36
  else
    l_intCount := l_xml.Count - 1;

  if l_xml.Count = 0 then
  begin
    l_str := l_str + NewLine + 'Empty';
    Result := PWideChar(l_str);
    Exit
  end
  else begin
    l_str := l_str + NewLine + 'Number of Vectors: ' +
      IntToStr(l_xml.Count) + NewLine + 'Total Vectors: ' +
      GetResultVector(l_xml) + NewLine + 'Contents: ';

    for l_intIndexer := 0 to l_intCount do
    begin
      l_str := l_str +
        VectorToStr(l_xml.Vector[l_intIndexer]);
    end;

    if not l_blnGreat then
    begin
      Delete(l_str, Length(l_str) - 1, 1);
      l_str := l_str + '.';
    end
    else
      l_str := l_str + '...';
  end;

  Result := PWideChar(l_str);
end;

function TInfoTipHandler.GetInfoTip(dwFlags: DWORD; var ppwszTip: PWideChar):
  HResult;
begin
  Result := S_OK;
  if (CompareText(ExtractFileExt(FFileName), '.vector') = 0) and
    Assigned(FMalloc) then
  begin
    ppwszTip := GetInformation(FFileName);
  end;
end;

function TInfoTipHandler.GetClassID(out classID: TCLSID): HResult;
begin
  classID := Class_InfoTipHandler;
  Result := S_OK;
end;

function TInfoTipHandler.GetCurFile(out pszFileName: POleStr): HResult;
begin
  Result := E_NOTIMPL;
end;

function TInfoTipHandler.GetInfoFlags(out pdwFlags: DWORD): HResult;
begin
  Result := E_NOTIMPL;
end;

procedure TInfoTipHandler.Initialize;
begin
  inherited;
  SHGetMalloc(FMalloc);
end;

function TInfoTipHandler.IsDirty: HResult;
begin
  Result := E_NOTIMPL;
end;

function TInfoTipHandler.Load(pszFileName: POleStr;
  dwMode: Integer): HResult;
begin
  FFileName := pszFileName;
  Result := S_OK;
end;

function TInfoTipHandler.Save(pszFileName: POleStr;
  fRemember: BOOL): HResult;
begin
  Result := E_NOTIMPL;
end;

function TInfoTipHandler.SaveCompleted(pszFileName: POleStr): HResult;
begin
  Result := E_NOTIMPL;
end;

{ TInfoTipFactory }

function TInfoTipFactory.GetProgID: string;
begin
  Result := '';
end;

procedure TInfoTipFactory.UpdateRegistry(Register: Boolean);
var
  ClsID: string;
begin
  ClsID := GUIDToString(ClassID);
  inherited UpdateRegistry(Register);
  ApproveShellExtension(Register, ClsID);
  if Register then
  begin
    CreateRegKey('.vector\shellex\{00021500-0000-0000-C000-000000000046}',
      '', ClsID);
  end
  else begin
    DeleteRegKey('.vector\shellex\{00021500-0000-0000-C000-000000000046}');
  end;
end;

procedure TInfoTipFactory.ApproveShellExtension(Register: Boolean;
  const ClsID: string);
const
  SApproveKey =
  'SOFTWARE\Microsoft\Windows\CurrentVersion\Shell Extensions\Approved';
begin
  with TRegistry.Create do
    try
      RootKey := HKEY_LOCAL_MACHINE;
      if not OpenKey(SApproveKey, True) then Exit;
      if Register then WriteString(ClsID, Description)
      else DeleteValue(ClsID);
    finally
      Free;
    end;
end;

initialization
  TInfoTipFactory.Create(ComServer, TInfoTipHandler, Class_InfoTipHandler,
    'VectorInfoTipHandler', 'Vector Information Tip handler',
    ciMultiInstance, tmApartment);
end.


