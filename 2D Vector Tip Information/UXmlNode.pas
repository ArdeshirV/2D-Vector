// Copyright 2002-2004 ArdeshirV, Licensed Under MIT
// https://github.com/ArdeshirV/2D-Vector
unit UXmlNode;

interface

uses xmldom, XMLDoc, XMLIntf;

type

{ Forward Decls }

  IXMLVectorsType = interface;
  IXMLVectorType = interface;
  IXMLStartPointType = interface;
  IXMLEndPointType = interface;

{ IXMLVectorsType }

  IXMLVectorsType = interface(IXMLNodeCollection)
    ['{708ADF80-7731-4714-8414-03B936456F97}']
    { Property Accessors }
    function Get_Vector(Index: Integer): IXMLVectorType;
    { Methods & Properties }
    function Add: IXMLVectorType;
    function Insert(const Index: Integer): IXMLVectorType;
    property Vector[Index: Integer]: IXMLVectorType read Get_Vector; default;
  end;

{ IXMLVectorType }

  IXMLVectorType = interface(IXMLNode)
    ['{232E734B-9C2E-4D58-A6E3-061A06A73A75}']
    { Property Accessors }
    function Get_StartPoint: IXMLStartPointType;
    function Get_EndPoint: IXMLEndPointType;
    function Get_ID: Integer;
    procedure Set_ID(Value: Integer);
    { Methods & Properties }
    property StartPoint: IXMLStartPointType read Get_StartPoint;
    property EndPoint: IXMLEndPointType read Get_EndPoint;
    property ID: Integer read Get_ID write Set_ID;
  end;

{ IXMLStartPointType }

  IXMLStartPointType = interface(IXMLNode)
    ['{672A49C6-CFFC-4CC8-B59F-9BCCE7639533}']
    { Property Accessors }
    function Get_X: Double;
    function Get_Y: Double;
    procedure Set_X(Value: Double);
    procedure Set_Y(Value: Double);
    { Methods & Properties }
    property X: Double read Get_X write Set_X;
    property Y: Double read Get_Y write Set_Y;
  end;

{ IXMLEndPointType }

  IXMLEndPointType = interface(IXMLNode)
    ['{6DC293AA-0C12-49D5-A258-2265C4E09833}']
    { Property Accessors }
    function Get_X: Double;
    function Get_Y: Double;
    procedure Set_X(Value: Double);
    procedure Set_Y(Value: Double);
    { Methods & Properties }
    property X: Double read Get_X write Set_X;
    property Y: Double read Get_Y write Set_Y;
  end;

{ Forward Decls }

  TXMLVectorsType = class;
  TXMLVectorType = class;
  TXMLStartPointType = class;
  TXMLEndPointType = class;

{ TXMLVectorsType }

  TXMLVectorsType = class(TXMLNodeCollection, IXMLVectorsType)
  protected
    { IXMLVectorsType }
    function Get_Vector(Index: Integer): IXMLVectorType;
    function Add: IXMLVectorType;
    function Insert(const Index: Integer): IXMLVectorType;
  public
    procedure AfterConstruction; override;
  end;

{ TXMLVectorType }

  TXMLVectorType = class(TXMLNode, IXMLVectorType)
  protected
    { IXMLVectorType }
    function Get_StartPoint: IXMLStartPointType;
    function Get_EndPoint: IXMLEndPointType;
    function Get_ID: Integer;
    procedure Set_ID(Value: Integer);
  public
    procedure AfterConstruction; override;
  end;

{ TXMLStartPointType }

  TXMLStartPointType = class(TXMLNode, IXMLStartPointType)
  protected
    { IXMLStartPointType }
    function Get_X: Double;
    function Get_Y: Double;
    procedure Set_X(Value: Double);
    procedure Set_Y(Value: Double);
  end;

{ TXMLEndPointType }

  TXMLEndPointType = class(TXMLNode, IXMLEndPointType)
  protected
    { IXMLEndPointType }
    function Get_X: Double;
    function Get_Y: Double;
    procedure Set_X(Value: Double);
    procedure Set_Y(Value: Double);
  end;

{ Global Functions }

function Getvectors(Doc: IXMLDocument): IXMLVectorsType;
function Loadvectors(const FileName: WideString): IXMLVectorsType;
function Newvectors: IXMLVectorsType;

const
  TargetNamespace = '';

implementation

{ Global Functions }

function Getvectors(Doc: IXMLDocument): IXMLVectorsType;
begin
  Result := Doc.GetDocBinding('vectors', TXMLVectorsType, TargetNamespace) as IXMLVectorsType;
end;

function Loadvectors(const FileName: WideString): IXMLVectorsType;
begin
  Result := LoadXMLDocument(FileName).GetDocBinding('vectors', TXMLVectorsType,
    TargetNamespace) as IXMLVectorsType;
end;

function Newvectors: IXMLVectorsType;
begin
  Result := NewXMLDocument.GetDocBinding('vectors', TXMLVectorsType,
    TargetNamespace) as IXMLVectorsType;
end;

{ TXMLVectorsType }

procedure TXMLVectorsType.AfterConstruction;
begin
  RegisterChildNode('vector', TXMLVectorType);
  ItemTag := 'vector';
  ItemInterface := IXMLVectorType;
  inherited;
end;

function TXMLVectorsType.Get_Vector(Index: Integer): IXMLVectorType;
begin
  Result := List[Index] as IXMLVectorType;
end;

function TXMLVectorsType.Add: IXMLVectorType;
begin
  Result := AddItem(-1) as IXMLVectorType;
end;

function TXMLVectorsType.Insert(const Index: Integer): IXMLVectorType;
begin
  Result := AddItem(Index) as IXMLVectorType;
end;

{ TXMLVectorType }

procedure TXMLVectorType.AfterConstruction;
begin
  RegisterChildNode('StartPoint', TXMLStartPointType);
  RegisterChildNode('EndPoint', TXMLEndPointType);
  inherited;
end;

function TXMLVectorType.Get_StartPoint: IXMLStartPointType;
begin
  Result := ChildNodes['StartPoint'] as IXMLStartPointType;
end;

function TXMLVectorType.Get_EndPoint: IXMLEndPointType;
begin
  Result := ChildNodes['EndPoint'] as IXMLEndPointType;
end;

function TXMLVectorType.Get_ID: Integer;
begin
  Result := ChildNodes['ID'].NodeValue;
end;

procedure TXMLVectorType.Set_ID(Value: Integer);
begin
  ChildNodes['ID'].NodeValue := Value;
end;

{ TXMLStartPointType }

function TXMLStartPointType.Get_X: Double;
begin
  Result := ChildNodes['X'].NodeValue;
end;

procedure TXMLStartPointType.Set_X(Value: Double);
begin
  ChildNodes['X'].NodeValue := Value;
end;

function TXMLStartPointType.Get_Y: Double;
begin
  Result := ChildNodes['Y'].NodeValue;
end;

procedure TXMLStartPointType.Set_Y(Value: Double);
begin
  ChildNodes['Y'].NodeValue := Value;
end;

{ TXMLEndPointType }

function TXMLEndPointType.Get_X: Double;
begin
  Result := ChildNodes['X'].NodeValue;
end;

procedure TXMLEndPointType.Set_X(Value: Double);
begin
  ChildNodes['X'].NodeValue := Value;
end;

function TXMLEndPointType.Get_Y: Double;
begin
  Result := ChildNodes['Y'].NodeValue;
end;

procedure TXMLEndPointType.Set_Y(Value: Double);
begin
  ChildNodes['Y'].NodeValue := Value;
end;

end.


