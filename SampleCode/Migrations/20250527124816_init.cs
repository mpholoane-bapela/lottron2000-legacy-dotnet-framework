using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingBots.Native.Infra.DbAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "logic");

            migrationBuilder.CreateTable(
                name: "Bot_01_Main",
                schema: "logic",
                columns: table => new
                {
                    BotID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TradingPair = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    PriceLimit_DealOpen = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    TakeProfit_DealClose = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InstanceGap_DealOpen = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bot_01_Main", x => x.BotID);
                });

            migrationBuilder.CreateTable(
                name: "CompletedDeal",
                schema: "logic",
                columns: table => new
                {
                    CompletedDealID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Instance_Priority = table.Column<int>(type: "int", nullable: false),
                    ProfitAmount_DCA = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    DateTime_Completed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeToComplete_Seconds = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    ROC_Instance_DCA_Fraction = table.Column<decimal>(type: "decimal(14,6)", nullable: false),
                    ROC_Bot_DCA_Fraction = table.Column<decimal>(type: "decimal(14,6)", nullable: false),
                    ROC_Account_DCA_Fraction = table.Column<decimal>(type: "decimal(14,6)", nullable: false),
                    TradingFees_Total = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    TradingFees_To_ProfitAmount_DCA = table.Column<decimal>(type: "decimal(14,6)", nullable: false),
                    ProfitAmount_FractionOfTotal_DCA = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ProfitAmount_SingleDeal = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ROC_Instance_SingleDeal_Fraction = table.Column<decimal>(type: "decimal(14,6)", nullable: false),
                    ROC_Bot_SingleDeal_Fraction = table.Column<decimal>(type: "decimal(14,6)", nullable: false),
                    ROC_Account_SingleDeal_Fraction = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TradingFees_To_ProfitAmount_SingleDeal = table.Column<decimal>(type: "decimal(14,6)", nullable: false),
                    ProfitAmount_FractionOfTotal_SingleDeal = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    DealOpen_Price = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    DealClose_Price = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    Amount_Total_Calibrated = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    Amount_Total_Organic = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    DealClose_Method = table.Column<int>(type: "int", nullable: false),
                    CalibrationFactor = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Instance_TTP_Extra_Fraction = table.Column<decimal>(type: "decimal(14,6)", nullable: false),
                    Volume_Calibrated = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    Volume_Organic = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    Amount_CurrencySymbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateTime_Started = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeToComplete_TimeSpan = table.Column<long>(type: "bigint", nullable: false),
                    BotID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BotInstanceID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DealID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApiOrderID_DealOpen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApiOrderID_DealClose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChangeFraction_Organic = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    ChangeFraction_Calibrated = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    BotSetUp_InstanceGap_Trading = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    BotSetUp_InstanceGap_Absolute = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    BotSetUp_TPCondition = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    BotSetUp_TTP_Active = table.Column<bool>(type: "bit", nullable: false),
                    BotSetUp_TTP_ActivationPoint_OffsetPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BotSetUp_TTP_TradeTriggerPoint_OffsetPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BotSetUp_TTP_EventualTrade_OffsetPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Instance_TTP_EventualTrade_Price = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    BotSetUp_TradingPair = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BotSetUp_TradeTimeSpan = table.Column<int>(type: "int", nullable: false),
                    BotSetUp_TradeDirection = table.Column<int>(type: "int", nullable: false),
                    BotSetUp_StopLoss_TriggerChangeAmount = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    BotSetUp_StopOrder_Trigger_Price = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    BotSetUp_StopLoss_EventualTradePrice = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    AccountBalance_StarOfTrading = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedByUserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    __CompletedDeal_Type = table.Column<int>(type: "int", nullable: false),
                    DbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedDeal", x => x.CompletedDealID);
                });

            migrationBuilder.CreateTable(
                name: "RecommendationAction",
                schema: "logic",
                columns: table => new
                {
                    RecommendationActionID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DealID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DealOpen_RecomTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DealClose_RecomTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DealOpen_ActTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DealClose_ActTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedByUserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationAction", x => x.RecommendationActionID);
                });

            migrationBuilder.CreateTable(
                name: "RecommendationFilter",
                schema: "logic",
                columns: table => new
                {
                    RecommendationFilterID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TradingPair_Domain = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SourceData_TradeExchange = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TradeDirection = table.Column<int>(type: "int", nullable: false),
                    MarketCategory = table.Column<int>(type: "int", nullable: false),
                    TimeInterval = table.Column<int>(type: "int", nullable: false),
                    DealActionType = table.Column<int>(type: "int", nullable: false),
                    AttributeToFilter = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    ValueAsString = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RelationalOperator = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedByUserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationFilter", x => x.RecommendationFilterID);
                });

            migrationBuilder.CreateTable(
                name: "ReOrdersCategory",
                schema: "logic",
                columns: table => new
                {
                    ReOrdersCategoryID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    PriceReference = table.Column<int>(type: "int", nullable: false),
                    AmountOfPosition_Estimate = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    DealClose_StartOffSet = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    DealClose_SpecificStartPrice = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    NumberOfOrders = table.Column<int>(type: "int", nullable: false),
                    OrdersGap_Percent = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    OverallPriority = table.Column<int>(type: "int", nullable: false),
                    DoPlaceOrders = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedByUserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReOrdersCategory", x => x.ReOrdersCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "ReOrdersMain",
                schema: "logic",
                columns: table => new
                {
                    ReOrdersMainID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PositionLimit = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedByUserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReOrdersMain", x => x.ReOrdersMainID);
                });

            migrationBuilder.CreateTable(
                name: "Bot_02_DynamicBehaviour",
                schema: "logic",
                columns: table => new
                {
                    BotID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TradeDirection = table.Column<int>(type: "int", nullable: false),
                    OrderTrigger_DealOpen = table.Column<int>(type: "int", nullable: false),
                    OrderTrigger_DealClose = table.Column<int>(type: "int", nullable: false),
                    OrderExecution_DealOpen = table.Column<int>(type: "int", nullable: false),
                    OrderExecution_DealClose = table.Column<int>(type: "int", nullable: false),
                    DealOpen_OffsetPercentToMarket = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    DealClose_OffsetPercentToMarket = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    MinRank_DealOpen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bot_02_DynamicBehaviour", x => x.BotID);
                    table.ForeignKey(
                        name: "FK_Bot_02_DynamicBehaviour_Bot_01_Main_BotID",
                        column: x => x.BotID,
                        principalSchema: "logic",
                        principalTable: "Bot_01_Main",
                        principalColumn: "BotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bot_03_PosSizeTracking",
                schema: "logic",
                columns: table => new
                {
                    BotID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DealOpen_Active = table.Column<bool>(type: "bit", nullable: false),
                    DealOpen_TotalPosSize_Max_Calibrated = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    DealOpen_Price_MinOffSetPercent = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    DealClose_OffSetMethod = table.Column<int>(type: "int", nullable: false),
                    DealClose_TotalPosSize_Min_Calibrated = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    DealClose_Price_MinOffSetPercent = table.Column<decimal>(type: "decimal(8,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bot_03_PosSizeTracking", x => x.BotID);
                    table.ForeignKey(
                        name: "FK_Bot_03_PosSizeTracking_Bot_01_Main_BotID",
                        column: x => x.BotID,
                        principalSchema: "logic",
                        principalTable: "Bot_01_Main",
                        principalColumn: "BotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bot_04_CostAveraging",
                schema: "logic",
                columns: table => new
                {
                    BotID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Method = table.Column<int>(type: "int", nullable: false),
                    ScalingAmount = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Rndng_Volume = table.Column<int>(type: "int", nullable: false),
                    Rndng_PositionAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bot_04_CostAveraging", x => x.BotID);
                    table.ForeignKey(
                        name: "FK_Bot_04_CostAveraging_Bot_01_Main_BotID",
                        column: x => x.BotID,
                        principalSchema: "logic",
                        principalTable: "Bot_01_Main",
                        principalColumn: "BotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bot_05_Profit",
                schema: "logic",
                columns: table => new
                {
                    BotID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MarketCategory = table.Column<int>(type: "int", nullable: false),
                    Leverage_Factor = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ProfitReInvestment_Type = table.Column<int>(type: "int", nullable: false),
                    ProfitReinvestment_Percent = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    DealsTracking_Active = table.Column<bool>(type: "bit", nullable: false),
                    PartialTP_Volume_Rounding = table.Column<int>(type: "int", nullable: false),
                    PartialTP_RemainderAllocation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bot_05_Profit", x => x.BotID);
                    table.ForeignKey(
                        name: "FK_Bot_05_Profit_Bot_01_Main_BotID",
                        column: x => x.BotID,
                        principalSchema: "logic",
                        principalTable: "Bot_01_Main",
                        principalColumn: "BotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bot_06_Others",
                schema: "logic",
                columns: table => new
                {
                    BotID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TradeTimeSpan = table.Column<int>(type: "int", nullable: false),
                    QuoteCurrencyAsProductID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaseDealOpenPrice = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    DealClose_MinRank_RelToMarket = table.Column<int>(type: "int", nullable: false),
                    DelayedOrderTrigger_Active = table.Column<bool>(type: "bit", nullable: false),
                    DelayedOrderTrigger_PercentRange = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    DelayedOrderTrigger_ImmediateOrdersCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bot_06_Others", x => x.BotID);
                    table.ForeignKey(
                        name: "FK_Bot_06_Others_Bot_01_Main_BotID",
                        column: x => x.BotID,
                        principalSchema: "logic",
                        principalTable: "Bot_01_Main",
                        principalColumn: "BotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bot_07_ExchangeConfig",
                schema: "logic",
                columns: table => new
                {
                    BotID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TradeExchangeConfigurationID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Volume_BatchUnits = table.Column<int>(type: "int", nullable: false),
                    TradeExchange = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TradingFee_Percent = table.Column<decimal>(type: "decimal(6,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bot_07_ExchangeConfig", x => x.BotID);
                    table.ForeignKey(
                        name: "FK_Bot_07_ExchangeConfig_Bot_01_Main_BotID",
                        column: x => x.BotID,
                        principalSchema: "logic",
                        principalTable: "Bot_01_Main",
                        principalColumn: "BotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bot_99_VersionInfo",
                schema: "logic",
                columns: table => new
                {
                    BotID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedByUserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bot_99_VersionInfo", x => x.BotID);
                    table.ForeignKey(
                        name: "FK_Bot_99_VersionInfo_Bot_02_DynamicBehaviour_BotID",
                        column: x => x.BotID,
                        principalSchema: "logic",
                        principalTable: "Bot_02_DynamicBehaviour",
                        principalColumn: "BotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BotInstance",
                schema: "logic",
                columns: table => new
                {
                    BotInstanceID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InstanceGap_Percent = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    InstanceGap_Reference = table.Column<int>(type: "int", nullable: false),
                    OrderVolume_Organic = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    OrderVolume_Calibrated = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    DealOpen_Price_Initial = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    DealOpen_Price_Final = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    BudgetPercentageOfParent = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    BudgetAllocationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DealOpen_FeeAmount = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedByUserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BotID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotInstance", x => x.BotInstanceID);
                    table.ForeignKey(
                        name: "FK_BotInstance_Bot_02_DynamicBehaviour_BotID",
                        column: x => x.BotID,
                        principalSchema: "logic",
                        principalTable: "Bot_02_DynamicBehaviour",
                        principalColumn: "BotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlobalStopLoss",
                schema: "logic",
                columns: table => new
                {
                    GlobalStopLossID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    StopLossType = table.Column<int>(type: "int", nullable: false),
                    PositionAmount_Percent = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    PositionAmount_Reserved = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    ActivationPoint_OffSetPercent = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    TakeLossPoint_OffSetPercent = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    NumberOfRuns_Max = table.Column<int>(type: "int", nullable: false),
                    StopLossReference = table.Column<int>(type: "int", nullable: false),
                    Activation_CalculatedPrice = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    TakeLoss_CalculatedPrice = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    TakeLoss_FinalPrice = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    NumberOfRuns_Current = table.Column<int>(type: "int", nullable: false),
                    Volume_Calculated = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    WorkflowStatus = table.Column<int>(type: "int", nullable: false),
                    DealID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiOrderID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiTimeStamp_Current = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiTimeStamp_DealOpen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiTimeStamp_DealClose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CircuitBreaker_Active = table.Column<bool>(type: "bit", nullable: false),
                    CircuitBreaker_TimeOut_Seconds = table.Column<int>(type: "int", nullable: false),
                    CircuitBreaker_TimeOut_StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CircuitBreaker_TimeOut_EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedByUserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BotID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalStopLoss", x => x.GlobalStopLossID);
                    table.ForeignKey(
                        name: "FK_GlobalStopLoss_Bot_02_DynamicBehaviour_BotID",
                        column: x => x.BotID,
                        principalSchema: "logic",
                        principalTable: "Bot_02_DynamicBehaviour",
                        principalColumn: "BotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BotInstanceState",
                schema: "logic",
                columns: table => new
                {
                    BotInstanceStateID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BotInstanceID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TradeWorkflowStatus = table.Column<int>(type: "int", nullable: false),
                    PrevState = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    DealID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApiOrderID_Current = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiOrderID_DealOpen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiOrderID_DealClose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiTimeStamp_Current = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApiTimeStamp_DealOpen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApiTimeStamp_DealClose = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedByUserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotInstanceState", x => x.BotInstanceStateID);
                    table.ForeignKey(
                        name: "FK_BotInstanceState_BotInstance_BotInstanceID",
                        column: x => x.BotInstanceID,
                        principalSchema: "logic",
                        principalTable: "BotInstance",
                        principalColumn: "BotInstanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BotPartialTP_00_Root",
                schema: "logic",
                columns: table => new
                {
                    BotPartialTPID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BotInstanceID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedByUserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotPartialTP_00_Root", x => x.BotPartialTPID);
                    table.ForeignKey(
                        name: "FK_BotPartialTP_00_Root_BotInstance_BotInstanceID",
                        column: x => x.BotInstanceID,
                        principalSchema: "logic",
                        principalTable: "BotInstance",
                        principalColumn: "BotInstanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BotPartialTP_01_Main",
                schema: "logic",
                columns: table => new
                {
                    BotPartialTPID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority_Partial = table.Column<int>(type: "int", nullable: false),
                    Priority_Instance = table.Column<int>(type: "int", nullable: false),
                    TakeProfit_Percent = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    Volume_CalcParameter_Percent = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    OrderTrigger_DealClose = table.Column<int>(type: "int", nullable: false),
                    OrderExecution_DealClose = table.Column<int>(type: "int", nullable: false),
                    DealClose_OffsetPercentToMarket = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    Volume_CalcResult_Amount = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    ReferencePoint_Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotPartialTP_01_Main", x => x.BotPartialTPID);
                    table.ForeignKey(
                        name: "FK_BotPartialTP_01_Main_BotPartialTP_00_Root_BotPartialTPID",
                        column: x => x.BotPartialTPID,
                        principalSchema: "logic",
                        principalTable: "BotPartialTP_00_Root",
                        principalColumn: "BotPartialTPID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BotPartialTP_02_TrlngTP",
                schema: "logic",
                columns: table => new
                {
                    BotPartialTPID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority_Partial = table.Column<int>(type: "int", nullable: false),
                    Priority_Instance = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ActivationPoint_OffSetPercent = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    TriggerPoint_OffSetPercent = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    OrderPoint_OffSetPercent = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    EventualPrice_Trigger = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    EventualPrice_DealClose = table.Column<decimal>(type: "decimal(24,8)", nullable: false),
                    Attempts_Max = table.Column<int>(type: "int", nullable: false),
                    Attempts_Current = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotPartialTP_02_TrlngTP", x => x.BotPartialTPID);
                    table.ForeignKey(
                        name: "FK_BotPartialTP_02_TrlngTP_BotPartialTP_00_Root_BotPartialTPID",
                        column: x => x.BotPartialTPID,
                        principalSchema: "logic",
                        principalTable: "BotPartialTP_00_Root",
                        principalColumn: "BotPartialTPID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BotPartialTP_03_State",
                schema: "logic",
                columns: table => new
                {
                    BotPartialTPID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority_Partial = table.Column<int>(type: "int", nullable: false),
                    Priority_Instance = table.Column<int>(type: "int", nullable: false),
                    PartialTpStatus = table.Column<int>(type: "int", nullable: false),
                    PrevState = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    DealID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiOrderID_Current = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiOrderID_DealOpen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiOrderID_DealClose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiTimeStamp_Current = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiTimeStamp_DealOpen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiTimeStamp_DealClose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DealClose_Price_Initial = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    DealClose_Price_Initial_Relevant = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    DealClose_Price_Final = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    Volume_ActualClosed = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    Profit_EstAmount = table.Column<decimal>(type: "decimal(9,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotPartialTP_03_State", x => x.BotPartialTPID);
                    table.ForeignKey(
                        name: "FK_BotPartialTP_03_State_BotPartialTP_00_Root_BotPartialTPID",
                        column: x => x.BotPartialTPID,
                        principalSchema: "logic",
                        principalTable: "BotPartialTP_00_Root",
                        principalColumn: "BotPartialTPID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BotPartialTP_04_PosSizeTrck",
                schema: "logic",
                columns: table => new
                {
                    BotPartialTPID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority_Partial = table.Column<int>(type: "int", nullable: false),
                    Priority_Instance = table.Column<int>(type: "int", nullable: false),
                    DealClose_OffSetMethod = table.Column<int>(type: "int", nullable: false),
                    DealClose_Price_MinOffSetPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotPartialTP_04_PosSizeTrck", x => x.BotPartialTPID);
                    table.ForeignKey(
                        name: "FK_BotPartialTP_04_PosSizeTrck_BotPartialTP_00_Root_BotPartialTPID",
                        column: x => x.BotPartialTPID,
                        principalSchema: "logic",
                        principalTable: "BotPartialTP_00_Root",
                        principalColumn: "BotPartialTPID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotInstance_BotID",
                schema: "logic",
                table: "BotInstance",
                column: "BotID");

            migrationBuilder.CreateIndex(
                name: "IX_BotInstanceState_BotInstanceID",
                schema: "logic",
                table: "BotInstanceState",
                column: "BotInstanceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BotPartialTP_00_Root_BotInstanceID",
                schema: "logic",
                table: "BotPartialTP_00_Root",
                column: "BotInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalStopLoss_BotID",
                schema: "logic",
                table: "GlobalStopLoss",
                column: "BotID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bot_03_PosSizeTracking",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "Bot_04_CostAveraging",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "Bot_05_Profit",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "Bot_06_Others",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "Bot_07_ExchangeConfig",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "Bot_99_VersionInfo",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "BotInstanceState",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "BotPartialTP_01_Main",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "BotPartialTP_02_TrlngTP",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "BotPartialTP_03_State",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "BotPartialTP_04_PosSizeTrck",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "CompletedDeal",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "GlobalStopLoss",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "RecommendationAction",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "RecommendationFilter",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "ReOrdersCategory",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "ReOrdersMain",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "BotPartialTP_00_Root",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "BotInstance",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "Bot_02_DynamicBehaviour",
                schema: "logic");

            migrationBuilder.DropTable(
                name: "Bot_01_Main",
                schema: "logic");
        }
    }
}
